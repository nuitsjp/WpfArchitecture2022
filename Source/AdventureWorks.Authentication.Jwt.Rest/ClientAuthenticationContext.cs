using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.Rest.Client;

/// <summary>
/// クライアント用認証コンテキスト
/// </summary>
public class ClientAuthenticationContext : IAuthenticationContext
{
    /// <summary>
    /// カレントユーザーを取得する。
    /// </summary>
    public User CurrentUser { get; internal set; } = default!;
    /// <summary>
    /// トークンを取得する。
    /// </summary>
    public string CurrentTokenString { get; internal set; } = default!;
}