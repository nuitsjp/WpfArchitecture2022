﻿using AdventureWorks.Database;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using Dapper;

namespace AdventureWorks.Purchasing.UseCase.SqlServer.RePurchasing;
public class RePurchasingQueryService : IRePurchasingQueryService
{
    private readonly IDatabase _database;

    public RePurchasingQueryService(IDatabase database)
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