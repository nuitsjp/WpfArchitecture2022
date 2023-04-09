using AdventureWorks.Business;

namespace AdventureWorks.Authentication;

/// <summary>
/// 認証コンテキスト。認証処理と認証状態を管理する。
/// </summary>
public interface IAuthenticationContext
{
    /// <summary>
    /// 認証済トークン
    /// </summary>
    string CurrentTokenString { get; }
    /// <summary>
    /// 認証済ユーザー
    /// </summary>
    User CurrentUser { get; }
    /// <summary>
    /// 認証処理を実施する。
    /// </summary>
    /// <param name="audience">トークンに署名する際のオーディエンス</param>
    /// <returns></returns>
    bool TryAuthenticate(string audience);
}

