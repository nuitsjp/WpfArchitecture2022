using System.Data;
using System.Transactions;

namespace AdventureWorks.Database;

public class Transaction : ITransaction
{
    private bool _disposed;
    private readonly TransactionScope _transactionScope;

    public Transaction(TransactionScope transactionScope, IDbConnection connection)
    {
        _transactionScope = transactionScope;
        Connection = connection;
    }

    public IDbConnection Connection { get; }

    public void Complete()
    {
        _transactionScope.Complete();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_disposed is false)
            {
                _transactionScope.Dispose();
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}