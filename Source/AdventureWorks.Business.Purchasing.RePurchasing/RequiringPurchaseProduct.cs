namespace AdventureWorks.Business.Purchasing.RePurchasing;

/// <summary>
/// 要再発注製品
/// </summary>
public record RequiringPurchaseProduct(
    VendorId VendorId,
    string VendorName,
    ProductCategoryId ProductCategoryId,
    string ProductCategoryName,
    ProductSubcategoryId ProductSubcategoryId,
    string ProductSubcategoryName,
    ProductId ProductId,
    string ProductName,
    Quantity PurchasingQuantity,
    Dollar UnitPrice,
    Days ShipmentResponseDays,
    Days AverageLeadTime,
    Quantity InventoryQuantity,
    Quantity UnclaimedPurchaseQuantity,
    DoubleQuantity AverageDailyShipmentQuantity)
{
    /// <summary>
    /// 小計
    /// </summary>
    public Dollar LineTotal => UnitPrice * PurchasingQuantity;
}
