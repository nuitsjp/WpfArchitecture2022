namespace AdventureWorks.Business.Purchasing;

public record PurchaseOrderDetail(
    PurchaseOrderId PurchaseOrderId,
    PurchaseOrderDetailId PurchaseOrderDetailId,
    Date DueDate,
    Quantity OrderQuantity,
    ProductId ProductId,
    Dollar UnitPrice,
    Dollar LineTotal,
    DecimalQuantity ReceiveQuantity,
    DecimalQuantity RejectedQuantity,
    DecimalQuantity StockedQuantity,
    ModifiedDateTime ModifiedDateTime)
{
    /// <summary>
    /// 未発注の発注明細をインスタンス化する。
    /// </summary>
    /// <param name="dueDate"></param>
    /// <param name="productId"></param>
    /// <param name="unitPrice"></param>
    /// <param name="orderQuantity"></param>
    public static PurchaseOrderDetail NewOrderDetail(
        Date dueDate,
        ProductId productId,
        Dollar unitPrice,
        Quantity orderQuantity)
    {
        return new(
            PurchaseOrderId.Unregistered,
            PurchaseOrderDetailId.Unregistered,
            dueDate,
            orderQuantity,
            productId,
            unitPrice,
            unitPrice * orderQuantity,
            new DecimalQuantity(0),
            new DecimalQuantity(0),
            new DecimalQuantity(0),
            ModifiedDateTime.Unregistered);
    }
}