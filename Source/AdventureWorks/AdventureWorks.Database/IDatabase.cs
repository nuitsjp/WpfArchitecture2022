namespace AdventureWorks.Database;

public interface IDatabase
{
    ITransaction BeginTransaction();
}