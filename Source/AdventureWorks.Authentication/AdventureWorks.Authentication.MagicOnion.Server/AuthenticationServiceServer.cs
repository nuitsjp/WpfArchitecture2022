using AdventureWorks.Authentication.MagicOnion.Client;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Authentication.MagicOnion.Server;

/// <summary>
/// �F�؃T�[�r�X�T�[�o�[�B
/// </summary>
/// <remarks>
/// �A�Z���u���[����͂��ăT�[�r�X�o�^���邽�߁A�Q�Ƃ��Ă���ӏ��������悤�Ɍ����邪���ۂɂ͗��p����Ă��邽��
/// �x����}������B
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