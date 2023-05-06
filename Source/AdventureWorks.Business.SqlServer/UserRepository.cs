using Dapper;

namespace AdventureWorks.Business.SqlServer;

/// <summary>
/// Userリポジトリー
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    /// 接続データベース
    /// </summary>
    private readonly AdventureWorksDatabase _database;

    /// <summary>
    /// インスタンスを生成する
    /// </summary>
    /// <param name="database"></param>
    public UserRepository(AdventureWorksDatabase database)
    {
        _database = database;
    }

    /// <summary>
    /// ユーザーを取得する
    /// </summary>
    /// <param name="loginId"></param>
    /// <returns></returns>
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
