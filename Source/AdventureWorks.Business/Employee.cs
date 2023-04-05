namespace AdventureWorks.Business;

public class Employee
{
    public Employee(EmployeeId id, LoginId loginId)
    {
        Id = id;
        LoginId = loginId;
    }

    public EmployeeId Id { get; }
    public LoginId LoginId { get; }
}