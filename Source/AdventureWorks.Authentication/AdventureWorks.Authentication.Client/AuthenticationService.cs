using AdventureWorks.Authentication.Service;

namespace AdventureWorks.Authentication.Client
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private Employee? _currentEmployee;

        public AuthenticationService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
            return await _employeeRepository.TryGetEmployeeByIdAsync(new LoginId(userName), out _currentEmployee);
        }
    }
}