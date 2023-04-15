using AdventureWorks.Authentication;
using AdventureWorks.Business;

namespace AdventureWorks.Hosting.AspNetCore;

public class ServerAuthenticationContext : IAuthenticationContext
{
    public static readonly ServerAuthenticationContext Instance = new();
    /// <summary>
    /// 認証済ユーザー
    /// </summary>
    private readonly AsyncLocal<User?> _currentUserAsyncLocal = new();

    /// <summary>
    /// 認証トークン
    /// </summary>
    private readonly AsyncLocal<string?> _currentTokenString = new();

    private ServerAuthenticationContext()
    {
    }

    /// <summary>
    /// 認証済ユーザーを取得する。
    /// </summary>
    public User CurrentUser
    {
        get
        {
            if (_currentUserAsyncLocal.Value is null)
            {
                throw new InvalidOperationException("認証処理の完了時に利用してください。");
            }

            return _currentUserAsyncLocal.Value;
        }

        set => _currentUserAsyncLocal.Value = value;
    }

    public string CurrentTokenString
    {
        get
        {
            if (_currentTokenString.Value is null)
            {
                throw new InvalidOperationException("認証処理の完了時に利用してください。");
            }

            return _currentTokenString.Value;
        }

        set => _currentTokenString.Value = value;
    }

    /// <summary>
    /// 認証済ユーザーをクリアする。
    /// </summary>
    public void ClearCurrentUser()
    {
        _currentUserAsyncLocal.Value = null;
        _currentTokenString.Value = null;
    }

    /// <summary>
    /// 認証済ユーザーかどうかを取得する。
    /// </summary>
    public bool IsAuthenticated => _currentUserAsyncLocal.Value is not null;
}