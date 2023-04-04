using System.Data;

namespace AdventureWorks.Database;

public interface ITransaction : IDisposable
{
    IDbConnection Connection { get; }
    void Complete();
}