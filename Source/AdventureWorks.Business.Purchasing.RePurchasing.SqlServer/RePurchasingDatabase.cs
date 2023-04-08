using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;

public class RePurchasingDatabase : Database.Database
{
    public RePurchasingDatabase(string userId, string password) : base(userId, password)
    {
    }
}