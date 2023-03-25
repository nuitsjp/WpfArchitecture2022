using AdventureWorks.Database;
using Dapper;

namespace AdventureWorks.SqlServer;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDatabase _database;

    public EmployeeRepository(IDatabase database)
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
}