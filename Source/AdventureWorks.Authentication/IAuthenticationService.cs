namespace AdventureWorks.Authentication;

/// <summary>
/// 認証サービス
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// 認証を試みる
    /// </summary>
    /// <returns></returns>
    Task<AuthenticateResult> TryAuthenticateAsync();
}