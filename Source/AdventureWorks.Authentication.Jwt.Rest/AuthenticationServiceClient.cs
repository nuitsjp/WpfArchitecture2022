using System.Net.Http;
using System.Windows;
using AdventureWorks.Authentication.Jwt.MagicOnion.Client;
using AdventureWorks.Business;
using AdventureWorks.Hosting;
using MagicOnion;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Authentication.Jwt.Rest;

/// <summary>
/// 認証処理RESTサービスを呼び出すためのクライアント
/// </summary>
public static class AuthenticationServiceClient
{

    public static IAuthenticationContext Authenticate(IApplicationBuilder builder)
    {
        try
        {
            var baseAddress = Environments.GetEnvironmentVariable(
                "AdventureWorks.Authentication.Jwt.Rest.BaseAddress",
                "https://localhost:4001");
            var context = AuthenticateAsync(baseAddress).Result;

            builder.Services.AddSingleton<IAuthenticationContext>(context);

            return context;
        }
        catch
        {
            MessageBox.Show(
                "ユーザー認証に失敗しました。",
                "認証エラー",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            // アプリケーションを終了する。
            Environment.Exit(1);

            // ここには到達しない。
            return default!;
        }
    }

    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    private static async Task<ClientAuthenticationContext> AuthenticateAsync(string baseAddress)
    {
        var token = await HttpClient.GetStringAsync($"{baseAddress}/Authentication");
        return new ClientAuthenticationContext(token, UserSerializer.Deserialize(token));
    }

    /// <summary>
    /// IAuthenticationContextのJWTによる実装。
    /// </summary>
    private class ClientAuthenticationContext : IClientAuthenticationContext
    {
        public ClientAuthenticationContext(string currentTokenString, User currentUser)
        {
            CurrentTokenString = currentTokenString;
            CurrentUser = currentUser;
        }

        /// <summary>
        /// 認証済みトークンを取得する。
        /// </summary>
        public string CurrentTokenString { get; }

        /// <summary>
        /// 認証済ユーザーを取得する。
        /// </summary>
        public User CurrentUser { get; }
    }

}