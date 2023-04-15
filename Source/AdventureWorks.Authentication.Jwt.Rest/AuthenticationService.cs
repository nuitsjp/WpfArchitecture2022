﻿using AdventureWorks.Business;
using AdventureWorks.MagicOnion.Client;

namespace AdventureWorks.Authentication.Jwt.Rest.Client;

public class AuthenticationService : IAuthenticationService
{
    /// <summary>
    /// Windows認証を有効化したHTTPクライアント
    /// </summary>
    private static readonly HttpClient HttpClient = new(new HttpClientHandler { UseDefaultCredentials = true });

    private readonly Audience _audience;

    private readonly AuthenticationContext _context = new ();

    public AuthenticationService(Audience audience)
    {
        _audience = audience;
    }

    public async Task<bool> TryAuthenticateAsync()
    {
        try
        {
            var baseAddress = Environments.GetEnvironmentVariable(
                "AdventureWorks.Authentication.Jwt.Rest.BaseAddress",
                "https://localhost:4001");
            var token = await HttpClient.GetStringAsync($"{baseAddress}/Authentication/{_audience.Value}");
            _context.CurrentTokenString = token;
            _context.CurrentUser = UserSerializer.Deserialize(token, _audience);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IAuthenticationContext Context => _context;

    private class AuthenticationContext : IAuthenticationContext
    {
        public User CurrentUser { get; internal set; } = default!;
        public string CurrentTokenString { get; internal set; } = default!;
    }
}