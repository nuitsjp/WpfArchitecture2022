namespace AdventureWorks.Authentication.Jwt.Rest;

/// <summary>
/// 認証サービス
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// 認証処理を行う。
    /// </summary>
    /// <returns>認証トークン</returns>
    Task<string> AuthenticateAsync();
}