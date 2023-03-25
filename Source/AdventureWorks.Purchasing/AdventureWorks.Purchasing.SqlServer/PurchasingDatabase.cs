using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Purchasing.SqlServer;

public class PurchasingDatabase : Database.Database
{
    public PurchasingDatabase(IConfiguration configuration, string userId, string password)
        : base(configuration, userId, password)
    {
    }
}