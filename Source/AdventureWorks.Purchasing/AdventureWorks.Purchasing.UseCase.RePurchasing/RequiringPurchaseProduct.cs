using AdventureWorks.Purchasing.Production;

namespace AdventureWorks.Purchasing.UseCase.RePurchasing;

public record RequiringPurchaseProduct
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vendorId"></param>
    /// <param name="vendorName"></param>
    /// <param name="productCategoryId"></param>
    /// <param name="productCategoryName"></param>
    /// <param name="productSubcategoryId"></param>
    /// <param name="productSubcategoryName"></param>
    /// <param name="productId"></param>
    /// <param name="productName"></param>
    /// <param name="purchasingQuantity"></param>
    /// <param name="unitPrice"></param>
    /// <param name="shipmentResponseDays"></param>
    /// <param name="averageLeadTime"></param>
    /// <param name="inventoryQuantity"></param>
    /// <param name="unclaimedPurchaseQuantity"></param>
    /// <param name="averageDailyShipmentQuantity"></param>
    public RequiringPurchaseProduct(
        VendorId vendorId,
        string vendorName,
        ProductCategoryId productCategoryId,
        string productCategoryName,
        ProductSubcategoryId productSubcategoryId,
        string productSubcategoryName,
        ProductId productId,
        string productName,
        Quantity purchasingQuantity, 
        Dollar unitPrice,
        Days shipmentResponseDays,
        Days averageLeadTime,
        Quantity inventoryQuantity,
        Quantity unclaimedPurchaseQuantity,
        DoubleQuantity averageDailyShipmentQuantity)
    {
        VendorId = vendorId;
        VendorName = vendorName;
        ProductCategoryId = productCategoryId;
        ProductCategoryName = productCategoryName;
        ProductSubcategoryId = productSubcategoryId;
        ProductSubcategoryName = productSubcategoryName;
        ProductId = productId;
        ProductName = productName;
        PurchasingQuantity = purchasingQuantity;
        UnitPrice = unitPrice;
        ShipmentResponseDays = shipmentResponseDays;
        AverageLeadTime = averageLeadTime;
        InventoryQuantity = inventoryQuantity;
        UnclaimedPurchaseQuantity = unclaimedPurchaseQuantity;
        AverageDailyShipmentQuantity = averageDailyShipmentQuantity;
    }

    public VendorId VendorId { get; }
    public string VendorName { get; }
    public ProductCategoryId ProductCategoryId { get; }
    public string ProductCategoryName { get; }
    public ProductSubcategoryId ProductSubcategoryId { get; }
    public string ProductSubcategoryName { get; }
    public ProductId ProductId { get; }
    public string ProductName { get; }
    public Quantity PurchasingQuantity { get; }
    public Dollar UnitPrice { get; }
    public Days ShipmentResponseDays { get; }
    public Days AverageLeadTime { get; }
    public Quantity InventoryQuantity { get; }
    public Quantity UnclaimedPurchaseQuantity { get; }
    public DoubleQuantity AverageDailyShipmentQuantity { get; }
    public Dollar LineTotal => UnitPrice * PurchasingQuantity;

}