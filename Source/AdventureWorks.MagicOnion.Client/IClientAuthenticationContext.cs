using AdventureWorks.Authentication;

namespace AdventureWorks.MagicOnion.Client;

public interface IClientAuthenticationContext : IAuthenticationContext
{
    /// <summary>
    /// 認証済みトークンを取得する。
    /// </summary>
    public string CurrentTokenString { get; }
}
