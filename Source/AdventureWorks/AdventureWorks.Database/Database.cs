using System.Data;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Database;

public class Database : IDatabase
{
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Database(IConfiguration configuration, string userId, string password)
    {
        _connectionString = ConnectionStringProvider.Resolve(configuration, userId, password);
    }

    public ITransaction BeginTransaction()
    {
        var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return new Transaction(scope, connection);
        }
        catch
        {
            // コネクション接続でエラーとなった場合、TransactionScopeを破棄する。
            scope.DisposeQuiet();
            throw;
        }
    }

    public IDbConnection Open()
    {
        IDbConnection connection = new SqlConnection(_connectionString);
        try
        {
            connection.Open();
            return connection;
        }
        catch
        {
            // Openに失敗した場合、Disposeは不要だと思われるが、念のため解放しておく。
            connection.DisposeQuiet();
            throw;
        }
    }
}