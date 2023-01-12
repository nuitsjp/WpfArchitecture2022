using AdventureWorks.Database;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AdventureWorks.Purchasing.UseCase.Database.RePurchasing;
public class RePurchasingQueryService : IRePurchasingQueryService
{
    private readonly IDatabase _database;

    public RePurchasingQueryService(IDatabase database)
    {
        _database = database;
    }

    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        using var transaction = _database.BeginTransaction();

        return (await transaction.Connection.QueryAsync<RequiringPurchaseProduct>(
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
