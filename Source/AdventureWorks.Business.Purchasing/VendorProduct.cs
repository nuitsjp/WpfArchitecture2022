namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// ベンダー取扱製品
/// </summary>
/// <param name="ProductId"></param>
/// <param name="AverageLeadTime"></param>
/// <param name="StandardPrice"></param>
/// <param name="LastReceiptCost"></param>
/// <param name="MinOrderQuantity"></param>
/// <param name="MaxOrderQuantity"></param>
/// <param name="OnOrderQuantity"></param>
/// <param name="UnitMeasureCode"></param>
/// <param name="ModifiedDateTime"></param>
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
