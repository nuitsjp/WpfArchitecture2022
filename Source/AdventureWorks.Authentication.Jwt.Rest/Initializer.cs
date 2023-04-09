using AdventureWorks.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Authentication.Jwt.Rest;

/// <summary>
/// JWTによる認証処理クライアントを初期化する。
/// </summary>
public static class Initializer
{
    /// <summary>
    /// JWTによる認証処理クライアントを初期化する。
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IAuthenticationContext Initialize(IApplicationBuilder builder)
    {
        // 認証コンテキストはシングルトンとして扱う。
        // 認証情報を保持しておく必要があるため。
        AuthenticationContext context = new();
        builder.Services.AddSingleton<IAuthenticationContext>(context);
        return context;
    }
}