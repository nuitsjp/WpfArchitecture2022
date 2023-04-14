using System.Security.Authentication;
using AdventureWorks.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Authentication.Jwt.Rest.Server;

/// <summary>
/// Windows�F�؂𗘗p����JWT�F�؏���REST�T�[�r�X
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    /// <summary>
    /// ���[�U�[���|�W�g���[
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// �C���X�^���X�𐶐�����B
    /// </summary>
    /// <param name="userRepository"></param>
    public AuthenticationController(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// �F�؏������s���B
    /// </summary>
    /// <returns></returns>
    /// <exception cref="AuthenticationException"></exception>
    [HttpGet()]
    public async Task<string> AuthenticateAsync()
    {
        var account = User.Identity!.Name!;
        if (await _userRepository.TryGetUserByIdAsync(new LoginId(account), out var user))
        {
            // �F�؂����������ꍇ�A���[�U�[����JWT�g�[�N���𐶐�����B
            return UserSerializer.Serialize(user, Properties.Resources.PrivateKey);
        }

        throw new AuthenticationException();
    }
}