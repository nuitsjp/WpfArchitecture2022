using AdventureWorks.Business;

namespace AdventureWorks.Authentication.Jwt.MagicOnion.Server;

public class AuthenticationContext : IAuthenticationContext
{
    /// <summary>
    /// 認証済トークン
    /// </summary>
    private string? _tokenString;
    /// <summary>
    /// 認証済ユーザー
    /// </summary>
    private User? _currentEmployee;

    /// <summary>
    /// 認証済みトークンを取得する。
    /// </summary>
    public string CurrentTokenString
    {
        get
        {
            if (_tokenString is null)
            {
                throw new InvalidOperationException("TryAuthenticateAsyncを正常終了後のみ利用可能です。");
            }

            return _tokenString;
        }

        internal set => _tokenString = value;
    }

    /// <summary>
    /// 認証済ユーザーを取得する。
    /// </summary>
    public User CurrentUser
    {
        get
        {
            if (_currentEmployee is null)
            {
                throw new InvalidOperationException("TryAuthenticateAsyncを正常終了後のみ利用可能です。");
            }

            return _currentEmployee;
        }

        internal set => _currentEmployee = value;
    }
}