using System.Security.Authentication;
using AdventureWorks.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Authentication.Jwt.Rest.Server;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase, IAuthenticationService
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUserRepository _userRepository;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet("{audience}")]
    public async Task<string> AuthenticateAsync(string audience)
    {
        if (await _userRepository.TryGetUserByIdAsync(new LoginId(User.Identity!.Name!), out var employee))
        {
            return UserSerializer.Serialize(employee, Properties.Resources.PrivateKey, audience);
        }

        throw new AuthenticationException();
    }
}