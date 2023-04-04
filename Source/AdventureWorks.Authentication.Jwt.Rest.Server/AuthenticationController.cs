using System.Security.Authentication;
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
    private readonly IEmployeeRepository _employeeRepository;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }

    [HttpGet(Name = IAuthenticationService.ServiceName)]
    public async Task<string> AuthenticateAsync()
    {
        if (await _employeeRepository.TryGetEmployeeByIdAsync(new LoginId(User.Identity!.Name!), out var employee))
        {
            return EmployeeSerializer.Serialize(employee, Properties.Resources.PrivateKey, "AdventureWorks.Authentication");
        }

        throw new AuthenticationException();
    }
}