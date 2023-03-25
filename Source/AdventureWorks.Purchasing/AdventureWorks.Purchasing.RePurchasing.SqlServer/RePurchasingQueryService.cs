using AdventureWorks.Database;
using Dapper;

namespace AdventureWorks.Purchasing.RePurchasing.SqlServer;
public class RePurchasingQueryService : IRePurchasingQueryService
{
    private readonly RePurchasingDatabase _database;

    public RePurchasingQueryService(RePurchasingDatabase database)
    {
        _database = database;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        using var connection = _database.Open();

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
    UnitPrice,
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
