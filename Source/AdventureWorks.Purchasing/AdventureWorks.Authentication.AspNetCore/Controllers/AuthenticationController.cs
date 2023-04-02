using AdventureWorks.Authentication.MagicOnion.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Authentication.AspNetCore.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(ILogger<AuthenticationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Authenticate")]
    public AuthenticationInfo Authenticate()
    {
        return 
            new(
                CryptoService.Encrypt(User.Identity!.Name!),
                string.Empty);
    }
}