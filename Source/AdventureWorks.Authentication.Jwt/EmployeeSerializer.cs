using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace AdventureWorks.Authentication.Jwt;

public static class EmployeeSerializer
{
    public static string Serialize(Employee employee, string privateKey, string audience)
    {
        // �������i���쐬����
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var key = new RsaSecurityKey(rsa);
        var credentials = new SigningCredentials(key, "RS256");

        // �N���[�����쐬����B
        ClaimsIdentity claimsIdentity = new();
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, employee.Id.AsPrimitive().ToString()));
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, "John Doe"));


        // �g�[�N���̑����I�u�W�F�N�g���쐬����B
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = typeof(IAuthenticationContext).Namespace,
            Audience = audience,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credentials,
        };

        // �g�[�N���𐶐�����B
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(descriptor);
        // �g�[�N���𕶎��񂩂���B
        return handler.WriteToken(token);
    }

    public static Employee Deserialize(string tokenString, string audience)
    {
        // �������ؗp�̌����쐬����B
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(Properties.Resources.PublicKey);
        var key = new RsaSecurityKey(rsa);


        // �g�[�N�����ؗp�p�����[�^
        var validationParams = new TokenValidationParameters
        {
            ValidIssuer = typeof(IAuthenticationContext).Namespace,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = key,
        };

        // �g�[�N�������؂��A���؍ς݃g�[�N�����擾����B
        // �g�[�N������p�̃N���X
        var handler = new JwtSecurityTokenHandler();
        handler.ValidateToken(tokenString, validationParams, out var token);
        var jwtSecurityToken = (JwtSecurityToken)token;

        var nameIdentity = jwtSecurityToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
        var name = jwtSecurityToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Name).Value;

        return new Employee(
            new EmployeeId(int.Parse(nameIdentity)),
            default!);
    }
}