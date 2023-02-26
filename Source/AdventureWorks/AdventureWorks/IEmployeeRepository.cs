namespace AdventureWorks
{
    public interface IEmployeeRepository
    {
        Task<bool> TryGetEmployeeByIdAsync(LoginId loginId, out Employee employee);
    }
}