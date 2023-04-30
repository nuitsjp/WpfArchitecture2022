namespace AdventureWorks.Business.Purchasing;

public record VendorProduct(
    ProductId ProductId,
    Days AverageLeadTime,
    Dollar StandardPrice,
    Dollar LastReceiptCost,
    Quantity MinOrderQuantity,
    Quantity MaxOrderQuantity,
    Quantity? OnOrderQuantity,
    UnitMeasureCode UnitMeasureCode,
    ModifiedDateTime ModifiedDateTime);
