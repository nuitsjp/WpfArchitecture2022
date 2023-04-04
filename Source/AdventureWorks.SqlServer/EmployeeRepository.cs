using Dapper;

namespace AdventureWorks.SqlServer;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AdventureWorksDatabase _database;

    public EmployeeRepository(AdventureWorksDatabase database)
    {
        _database = database;
    }

    public Task<bool> TryGetEmployeeByIdAsync(LoginId loginId, out Employee employee)
    {
        using var connection = _database.Open();

        var task = connection.QuerySingleOrDefaultAsync<Employee>(@"
select
	BusinessEntityID as Id,
	LoginID as LoginId
from
	HumanResources.Employee
where
	LoginID = @LoginId
",
            new
            {
                LoginId = loginId
            });
        task.Wait();
        employee = task.Result;

        return Task.FromResult(employee is not null);
    }

    public Task<bool> TryGetEmployeeIdByIdAsync(LoginId loginId, out EmployeeId employeeId)
    {
        using var connection = _database.Open();

        var task = connection.QuerySingleOrDefaultAsync(@"
select
	BusinessEntityID as Id
from
	HumanResources.Employee
where
	LoginID = @LoginId
",
            new
            {
                LoginId = loginId
            });
        task.Wait();
        employeeId = task.Result is null
            ? employeeId = default!
            : employeeId = new EmployeeId(task.Result.Id);

        return Task.FromResult(task.Result is not null);
    }
}