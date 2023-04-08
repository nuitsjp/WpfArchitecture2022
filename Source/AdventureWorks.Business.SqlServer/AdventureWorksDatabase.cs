namespace AdventureWorks.Business.SqlServer;

public class AdventureWorksDatabase : Database.Database
{
    public AdventureWorksDatabase(string userId, string password) : base(userId, password)
    {
    }
}