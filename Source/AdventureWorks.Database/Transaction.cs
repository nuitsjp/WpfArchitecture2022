using System.Data;
using System.Transactions;

namespace AdventureWorks.Database;

/// <summary>
/// トランザクション
/// </summary>
public class Transaction : ITransaction
{
    /// <summary>
    /// トランザクションの実態を提供するトランザクションスコープ
    /// </summary>
    private readonly TransactionScope _transactionScope;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="transactionScope"></param>
    /// <param name="connection"></param>
    public Transaction(TransactionScope transactionScope, IDbConnection connection)
    {
        _transactionScope = transactionScope;
        Connection = connection;
    }

    /// <summary>
    /// データベース接続
    /// </summary>
    public IDbConnection Connection { get; }

    /// <summary>
    /// トランザクションを完了する。
    /// </summary>
    public void Complete()
    {
        _transactionScope.Complete();
    }

    /// <summary>
    /// リソースを解放する。
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// リソースを解放する。
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;

        Connection.DisposeQuiet();
        _transactionScope.Dispose();
    }
}