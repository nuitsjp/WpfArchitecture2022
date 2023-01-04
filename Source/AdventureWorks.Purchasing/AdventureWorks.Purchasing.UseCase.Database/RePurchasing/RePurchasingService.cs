using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
public class RePurchasingService : IRePurchasingService
{
    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        var connectionString = new SqlConnectionStringBuilder
        {
            DataSource = "localhost",
            UserID = "sa",
            Password = "P@ssw0rd!",
            InitialCatalog = "AdventureWorks",
            TrustServerCertificate = true
        }.ToString();

        await using var connection = new SqlConnection(connectionString);
        connection.Open();

        return (await connection.QueryAsync<RequiringPurchaseProduct>(
            @"
select
    VendorId
from
    Purchasing.ProductRequiringPurchase"))
            .ToList();
    }
}
