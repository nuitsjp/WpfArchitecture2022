using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Server;

public class ServerAuthenticationContext : IAuthenticationContext
{
    /// <summary>
    /// 認証済ユーザー
    /// </summary>
    private readonly AsyncLocal<User> _currentUserAsyncLocal = new();

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
}