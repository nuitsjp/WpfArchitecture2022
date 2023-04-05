﻿using Dapper;

namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;
public class RePurchasingQuery : IRePurchasingQuery
{
    private readonly RePurchasingDatabase _database;

    public RePurchasingQuery(RePurchasingDatabase database)
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