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
    [HttpGet("{audience}")]
    public async Task<string> AuthenticateAsync(string audience)
    {
        var account = User.Identity!.Name!;
        var user = await _userRepository.GetUserAsync(new LoginId(account));
        if (user is null)
        {
            throw new AuthenticationException();
        }

        // �F�؂����������ꍇ�A���[�U�[����JWT�g�[�N���𐶐�����B
        // �����ł͔閧�������\�[�X����擾���Ă��܂����A���ۂɂ̓L�[�X�g�A�Ȃǂ𗘗p���Ă��������B
        return UserSerializer.Serialize(user, Properties.Resources.PrivateKey, new Audience(audience));
    }
}