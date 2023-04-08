using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Business.Purchasing.SqlServer;

public class PurchasingDatabase : Database.Database
{
    public PurchasingDatabase(string userId, string password) : base(userId, password)
    {
    }
}