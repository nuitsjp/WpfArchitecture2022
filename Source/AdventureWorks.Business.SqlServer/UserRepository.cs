using Dapper;

namespace AdventureWorks.Business.SqlServer;

public class UserRepository : IUserRepository
{
    private readonly AdventureWorksDatabase _database;

    public UserRepository(AdventureWorksDatabase database)
    {
        _database = database;
    }

    public Task<bool> TryGetUserByIdAsync(LoginId loginId, out User user)
    {
        using var connection = _database.Open();

        var task = connection.QuerySingleOrDefaultAsync<User>(@"
select
	BusinessEntityID as EmployeeId,
	'Adventure Works' as Name
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
        user = task.Result;

        return Task.FromResult(user is not null);
    }
}