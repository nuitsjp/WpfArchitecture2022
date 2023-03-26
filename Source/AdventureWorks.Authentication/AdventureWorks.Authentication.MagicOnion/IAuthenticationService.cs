using MagicOnion;

namespace AdventureWorks.Authentication.MagicOnion;

public interface IAuthenticationService : IService<IAuthenticationService>
{
    UnaryResult<Employee?> GetEmployeeAsync(LoginId loginId);
}