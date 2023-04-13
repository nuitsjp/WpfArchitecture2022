using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Server;

public class ServerAuthenticationContext : IAuthenticationContext
{
    public static readonly ServerAuthenticationContext Instance = new();
    /// <summary>
    /// 認証済ユーザー
    /// </summary>
    private readonly AsyncLocal<User?> _currentUserAsyncLocal = new();

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

        internal set => _currentUserAsyncLocal.Value = value;
    }

    /// <summary>
    /// 認証済ユーザーをクリアする。
    /// </summary>
    internal void ClearCurrentUser() => _currentUserAsyncLocal.Value = null;

    /// <summary>
    /// 認証済ユーザーかどうかを取得する。
    /// </summary>
    public bool IsAuthenticated => _currentUserAsyncLocal.Value is not null;
}