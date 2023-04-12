using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AdventureWorks.Business;
using Microsoft.IdentityModel.Tokens;

namespace AdventureWorks.Authentication.Jwt;

public static class UserSerializer
{
    /// <summary>
    /// aud属性は本来はサブシステムごとに個別に定義した方が好ましいが、ロギングAPIなどもを含めて制御するにはやや複雑になりすぎる。
    /// 今回はaud属性は固定で扱い、非固定で扱いたい場合は、そもそもWindows認証だけではなく、なんらかの認証プロバイダーの利用を検討
    /// をあわせて行うことにする。
    /// </summary>
    private const string Audience = "AdventureWorks";

    public static string Serialize(User user, string privateKey)
    {
        // 署名資格を作成する
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKey);
        var key = new RsaSecurityKey(rsa);
        var credentials = new SigningCredentials(key, "RS256");

        // クレームを作成する。
        ClaimsIdentity claimsIdentity = new();
        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.NameId, user.EmployeeId.AsPrimitive().ToString()));


        // トークンの属性オブジェクトを作成する。
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = typeof(IAuthenticationContext).Namespace,
            Audience = Audience,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credentials,
        };

        // トークンを生成する。
        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateJwtSecurityToken(descriptor);
        // トークンを文字列かする。
        return handler.WriteToken(token);
    }

    public static User Deserialize(string tokenString)
    {
        // 署名検証用の鍵を作成する。
        var rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(Properties.Resources.PublicKey);
        var key = new RsaSecurityKey(rsa);


        // トークン検証用パラメータ
        var validationParams = new TokenValidationParameters
        {
            ValidIssuer = typeof(IAuthenticationContext).Namespace,
            ValidAudience = Audience,
            ValidateLifetime = true,
            IssuerSigningKey = key,
        };

        // トークンを検証し、検証済みトークンを取得する。
        // トークン操作用のクラス
        var handler = new JwtSecurityTokenHandler();
        handler.ValidateToken(tokenString, validationParams, out var token);
        var jwtSecurityToken = (JwtSecurityToken)token;

        var nameIdentity = jwtSecurityToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.NameId).Value;

        return new User(new EmployeeId(int.Parse(nameIdentity)));
    }
}