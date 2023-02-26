using AdventureWorks.Authentication.Service;

namespace AdventureWorks.Authentication.Client;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthenticationServiceServer _server;
    private Employee? _currentEmployee;

    public AuthenticationService(IAuthenticationServiceServer server)
    {
        _server = server;
    }

    public Employee CurrentEmployee
    {
        get
        {
            if (_currentEmployee is null)
            {
                throw new InvalidOperationException("TryAuthenticateAsyncを正常終了後のみ利用可能です。");
            }

            return _currentEmployee;
        }
    }

    public async Task<bool> TryAuthenticateAsync()
    {
        var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        _currentEmployee = await _server.GetEmployeeAsync(new LoginId(userName));

        return _currentEmployee is not null;
    }
}