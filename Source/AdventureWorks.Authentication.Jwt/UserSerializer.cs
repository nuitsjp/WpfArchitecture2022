using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AdventureWorks.Business;
using Microsoft.IdentityModel.Tokens;

namespace AdventureWorks.Authentication.Jwt;

public static class UserSerializer
{
    public static string Serialize(User user, string privateKey, Audience audience)
    {
        // �������i���쐬����
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var key = new RsaSecurityKey(rsa);
        var credentials = new SigningCredentials(key, "RS256");

        // �N���[�����쐬����B
        ClaimsIdentity claimsIdentity = new();
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, user.EmployeeId.AsPrimitive().ToString()));


        // �g�[�N���̑����I�u�W�F�N�g���쐬����B
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = typeof(IAuthenticationContext).Namespace,
            Audience = audience.Value,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credentials,
        };

        // �g�[�N���𐶐�����B
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(descriptor);
        // �g�[�N���𕶎��񂩂���B
        return handler.WriteToken(token);
    }

    public static User Deserialize(string tokenString, Audience audience)
    {
        // �������ؗp�̌����쐬����B
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(Properties.Resources.PublicKey);
        var key = new RsaSecurityKey(rsa);


        // �g�[�N�����ؗp�p�����[�^
        var validationParams = new TokenValidationParameters
        {
            ValidIssuer = typeof(IAuthenticationContext).Namespace,
            ValidAudience = audience.Value,
            ValidateLifetime = true,
            IssuerSigningKey = key,
        };

        // �g�[�N�������؂��A���؍ς݃g�[�N�����擾����B
        // �g�[�N������p�̃N���X
        var handler = new JwtSecurityTokenHandler();
        handler.ValidateToken(tokenString, validationParams, out var token);
        var jwtSecurityToken = (JwtSecurityToken)token;

        var nameIdentity = jwtSecurityToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.NameId).Value;

        return new User(new EmployeeId(int.Parse(nameIdentity)));
    }
}