using AdventureWorks.Purchasing.Production;
using MagicOnion;
using MessagePack;

namespace AdventureWorks.Purchasing.UseCase.RePurchasing.Client;

public interface IRePurchasingQueryServiceServer : IService<IRePurchasingQueryServiceServer>
{
    UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync();
}

public record RequiringPurchaseProductDto
{
    public RequiringPurchaseProductDto(
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
};