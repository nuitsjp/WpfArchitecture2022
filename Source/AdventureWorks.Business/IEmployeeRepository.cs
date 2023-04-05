namespace AdventureWorks.Business
{
    public interface IEmployeeRepository
    {
        Task<bool> TryGetEmployeeByIdAsync(LoginId loginId, out Employee employee);

        Task<bool> TryGetEmployeeIdByIdAsync(LoginId loginId, out EmployeeId employeeId);
    }
}