using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Authentication.Jwt.AspNetCore.Controllers;

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