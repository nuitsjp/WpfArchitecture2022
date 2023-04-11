using AdventureWorks.Business;

namespace AdventureWorks.Authentication;

/// <summary>
/// 認証コンテキスト。認証処理と認証状態を管理する。
/// </summary>
public interface IAuthenticationContext
{
    /// <summary>
    /// 認証済ユーザー
    /// </summary>
    User CurrentUser { get; }
}


