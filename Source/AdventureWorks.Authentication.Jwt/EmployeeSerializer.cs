using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AdventureWorks.Business;
using Microsoft.IdentityModel.Tokens;

namespace AdventureWorks.Authentication.Jwt;

public static class EmployeeSerializer
{
    public static string Serialize(Employee employee, string privateKey, string audience)
    {
        // 署名資格を作成する
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var key = new RsaSecurityKey(rsa);
        var credentials = new SigningCredentials(key, "RS256");

        // クレームを作成する。
        ClaimsIdentity claimsIdentity = new();
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, employee.Id.AsPrimitive().ToString()));
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, "John Doe"));


        // トークンの属性オブジェクトを作成する。
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = typeof(IAuthenticationContext).Namespace,
            Audience = audience,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credentials,
        };

        // トークンを生成する。
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(descriptor);
        // トークンを文字列かする。
        return handler.WriteToken(token);
    }

    public static Employee Deserialize(string tokenString, string audience)
    {
        // 署名検証用の鍵を作成する。
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(Properties.Resources.PublicKey);
        var key = new RsaSecurityKey(rsa);


        // トークン検証用パラメータ
        var validationParams = new TokenValidationParameters
        {
            ValidIssuer = typeof(IAuthenticationContext).Namespace,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = key,
        };

        // トークンを検証し、検証済みトークンを取得する。
        // トークン操作用のクラス
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