﻿using Dapper;

namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;
public class RequiringPurchaseProductQuery : IRequiringPurchaseProductQuery
{
    private readonly RePurchasingDatabase _database;

    public RequiringPurchaseProductQuery(RePurchasingDatabase database)
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
    RePurchasing.vProductRequiringPurchase"))
            .ToList();
    }
}
