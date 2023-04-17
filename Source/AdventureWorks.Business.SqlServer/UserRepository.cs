using Dapper;

namespace AdventureWorks.Business.SqlServer;

public class UserRepository : IUserRepository
{
    private readonly AdventureWorksDatabase _database;

    public UserRepository(AdventureWorksDatabase database)
    {
        _database = database;
    }

    public async Task<User?> GetUserAsync(LoginId loginId)
    {
        using var connection = _database.Open();

        const string query = @"
select
	EmployeeId
from
	AdventureWorks.vUser
where
	LoginId = @LoginId
";
        return await connection.QuerySingleOrDefaultAsync<User>(
            query,
            new
            {
                LoginId = loginId
            });
    }
}

public partial struct ProductId
{
    
}

//public record ProductOrder(ProductId ProductId, EmployeeId EmployeeId);