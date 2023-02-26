using MagicOnion;

namespace AdventureWorks.Authentication.MagicOnion.Client;

public interface IAuthenticationServiceServer : IService<IAuthenticationServiceServer>
{
    UnaryResult<Employee?> GetEmployeeAsync(LoginId loginId);
}