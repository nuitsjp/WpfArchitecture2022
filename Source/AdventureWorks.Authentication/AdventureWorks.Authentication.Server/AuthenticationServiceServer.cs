using AdventureWorks.Authentication.Client;
using AdventureWorks.Authentication.Service;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Authentication.Server;

public class AuthenticationServiceServer : ServiceBase<IAuthenticationServiceServer>, IAuthenticationServiceServer
{
    private readonly IEmployeeRepository _employeeRepository;

    public AuthenticationServiceServer(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async UnaryResult<Employee?> GetEmployeeAsync(LoginId loginId)
    {
        if (await _employeeRepository.TryGetEmployeeByIdAsync(loginId, out var employee))
        {
            return employee;
        }
        return null;
    }
}