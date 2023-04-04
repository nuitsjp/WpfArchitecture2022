using System.Data;

namespace AdventureWorks.Database;

public interface IDatabase
{
    ITransaction BeginTransaction();
    IDbConnection Open();
}