using Dapper;

namespace AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;

/// <summary>
/// 要再発注製品取得クエリー
/// </summary>
public class RequiringPurchaseProductQuery : IRequiringPurchaseProductQuery
{
    /// <summary>
    /// 再発注データベース
    /// </summary>
    private readonly RePurchasingDatabase _database;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="database"></param>
    public RequiringPurchaseProductQuery(RePurchasingDatabase database)
    {
        _database = database;
    }

    /// <summary>
    /// 要再発注製品を取得する。
    /// </summary>
    /// <returns></returns>
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
