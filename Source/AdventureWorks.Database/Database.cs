using System.Data;
using System.Transactions;
using Microsoft.Data.SqlClient;

namespace AdventureWorks.Database;

/// <summary>
/// データベース
/// </summary>
public abstract class Database : IDatabase
{
    /// <summary>
    /// 接続文字列
    /// </summary>
    public string ConnectionString { get; }

    /// <summary>
    /// インスタンスを生成する。サーバーサイドでしか利用しない為、パスワードを直接保持する。
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    protected Database(string userId, string password)
    {
        ConnectionString = ConnectionStringProvider.Resolve(userId, password);
    }

    /// <summary>
    /// トランザクションを開始する。
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// データベース接続を開く。トランザクションは適用されないため、読み込み時に利用する。
    /// </summary>
    /// <returns></returns>
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