using System.Security.Authentication;
using AdventureWorks.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Authentication.Jwt.Rest.Server;

/// <summary>
/// Windows認証を利用したJWT認証処理RESTサービス
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase, IAuthenticationService
{
    /// <summary>
    /// ユーザーリポジトリー
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="userRepository"></param>
    public AuthenticationController(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// 認証処理を行う。
    /// </summary>
    /// <param name="audience">トークンの署名に利用するオーディエンス</param>
    /// <returns></returns>
    /// <exception cref="AuthenticationException"></exception>
    [HttpGet("{audience}")]
    public async Task<string> AuthenticateAsync(string audience)
    {
        var account = User.Identity!.Name!;
        if (await _userRepository.TryGetUserByIdAsync(new LoginId(User.Identity!.Name!), out var employee))
        {
            // 認証が成功した場合、ユーザーからJWTトークンを生成する。
            return UserSerializer.Serialize(employee, Properties.Resources.PrivateKey, audience);
        }

        throw new AuthenticationException();
    }
}