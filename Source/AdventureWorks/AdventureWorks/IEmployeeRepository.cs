namespace AdventureWorks.Authentication.Service
{
    public interface IEmployeeRepository
    {
        Task<bool> TryGetEmployeeByIdAsync(LoginId loginId, out Employee employee);
    }
}