﻿using AdventureWorks.Business;

namespace AdventureWorks.Authentication;

/// <summary>
/// 認証コンテキスト。認証処理と認証状態を管理する。
/// </summary>
public interface IAuthenticationContext
{
    /// <summary>
    /// 認証済ユーザーを取得する。
    /// </summary>
    User CurrentUser { get; }

    /// <summary>
    /// 認証済みトークンを取得する。
    /// </summary>
    public string CurrentTokenString { get; }
}