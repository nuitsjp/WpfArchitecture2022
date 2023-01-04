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
    VendorId,
    VendorName,
    ProductCategoryId,
    ProductCategoryName,
    ProductSubcategoryId,
    ProductSubcategoryName,
    ProductId,
    ProductName,
    PurchasingQuantity,
    ShipmentResponseDays,
    AverageLeadTime,
    InventoryQuantity,
    UnclaimedPurchaseQuantity,
    AverageDailyShipmentQuantity
from
    Purchasing.ProductRequiringPurchase"))
            .ToList();
    }
}
