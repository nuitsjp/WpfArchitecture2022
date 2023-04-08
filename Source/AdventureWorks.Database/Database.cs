using System.Data;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Database;

public abstract class Database : IDatabase
{
    public string ConnectionString { get; }

    protected Database(string userId, string password)
    {
        ConnectionString = ConnectionStringProvider.Resolve(userId, password);
    }

    public ITransaction BeginTransaction()
    {
        var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var connection = new SqlConnection(ConnectionString);
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
        IDbConnection connection = new SqlConnection(ConnectionString);
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