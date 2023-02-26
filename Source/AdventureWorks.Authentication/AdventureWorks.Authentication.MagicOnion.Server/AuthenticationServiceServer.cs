using AdventureWorks.Authentication.MagicOnion.Client;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Authentication.MagicOnion.Server;

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