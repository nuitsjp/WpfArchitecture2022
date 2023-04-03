using System.Security.Authentication;
using AdventureWorks.Authentication.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Authentication.WebApi;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
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

    [HttpGet(Name = "Authenticate")]
    public async Task<string> AuthenticateAsync()
    {
        if (await _employeeRepository.TryGetEmployeeByIdAsync(new LoginId(User.Identity!.Name!), out var employee))
        {
            return EmployeeSerializer.Serialize(employee, Properties.Resources.PrivateKey, "AdventureWorks.Authentication");
        }

        throw new AuthenticationException();
    }
}