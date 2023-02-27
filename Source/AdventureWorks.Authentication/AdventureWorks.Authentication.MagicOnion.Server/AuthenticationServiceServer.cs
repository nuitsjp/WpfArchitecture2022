using AdventureWorks.Authentication.MagicOnion.Client;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Authentication.MagicOnion.Server;

/// <summary>
/// 認証サービスサーバー。
/// </summary>
/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
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