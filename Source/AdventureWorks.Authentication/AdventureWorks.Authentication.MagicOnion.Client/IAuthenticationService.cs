using MagicOnion;

namespace AdventureWorks.Authentication.MagicOnion.Client;

public interface IAuthenticationService : IService<IAuthenticationService>
{
    UnaryResult<Employee?> GetEmployeeAsync(LoginId loginId);
}