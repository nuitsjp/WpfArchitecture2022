using MagicOnion;

namespace AdventureWorks.Authentication.Client;

public interface IAuthenticationServiceServer : IService<IAuthenticationServiceServer>
{
    UnaryResult<Employee?> GetEmployeeAsync(LoginId loginId);
}