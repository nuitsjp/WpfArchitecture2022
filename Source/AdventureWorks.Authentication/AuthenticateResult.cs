namespace AdventureWorks.Authentication;

/// <summary>
/// 認証結果
/// </summary>
/// <param name="IsAuthenticated"></param>
/// <param name="Context"></param>
public record AuthenticateResult(
    bool IsAuthenticated, 
    IAuthenticationContext Context);