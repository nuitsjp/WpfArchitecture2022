using System.Data;
// ReSharper disable UnusedMemberInSuper.Global

namespace AdventureWorks.Database;

public interface IDatabase
{
    ITransaction BeginTransaction();
    IDbConnection Open();
}