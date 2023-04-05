namespace AdventureWorks.Business.Purchasing;

public class VendorProduct
{
    public VendorProduct(
        ProductId productId, 
        Days averageLeadTime, 
        Dollar standardPrice, 
        Dollar lastReceiptCost, 
        Quantity minOrderQuantity,
        Quantity maxOrderQuantity,
        Quantity? onOrderQuantity, 
        UnitMeasureCode unitMeasureCode, 
        ModifiedDateTime modifiedDateTime)
    {
        ProductId = productId;
        AverageLeadTime = averageLeadTime;
        StandardPrice = standardPrice;
        LastReceiptCost = lastReceiptCost;
        MinOrderQuantity = minOrderQuantity;
        MaxOrderQuantity = maxOrderQuantity;
        OnOrderQuantity = onOrderQuantity;
        UnitMeasureCode = unitMeasureCode;
        ModifiedDateTime = modifiedDateTime;
    }

    public ProductId ProductId { get; }
    public Days AverageLeadTime { get; }
    public Dollar StandardPrice { get; }
    public Dollar LastReceiptCost { get; }
    public Quantity MinOrderQuantity { get; }
    public Quantity MaxOrderQuantity { get; }
    public Quantity? OnOrderQuantity { get; }
    public UnitMeasureCode UnitMeasureCode { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
}