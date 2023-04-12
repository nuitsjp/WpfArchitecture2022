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
                throw new InvalidOperationException("TryAuthenticateAsyncを正常終了後のみ利用可能です。");
            }

            return _currentUserAsyncLocal.Value;
        }

        internal set => _currentUserAsyncLocal.Value = value;
    }
}