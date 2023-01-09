using System.Transactions;
using Microsoft.Data.SqlClient;

namespace AdventureWorks.Database;

public class Database : IDatabase
{
    private readonly string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public ITransaction BeginTransaction()
    {
        var scope = new TransactionScope();
        try
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return new Transaction(scope, connection);
        }
        catch
        {
            // コネクション接続でエラーとなった場合、TransactionScopeを破棄する。
            scope.Dispose();
            throw;
        }
    }
}