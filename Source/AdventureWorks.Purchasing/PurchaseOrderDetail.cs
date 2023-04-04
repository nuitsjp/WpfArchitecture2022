namespace AdventureWorks.Purchasing;

public record PurchaseOrderDetail
{
    /// <summary>
    /// 既存の発注明細をインスタンス化する。
    /// </summary>
    /// <param name="purchaseOrderId"></param>
    /// <param name="purchaseOrderDetailId"></param>
    /// <param name="dueDate"></param>
    /// <param name="productId"></param>
    /// <param name="unitPrice"></param>
    /// <param name="orderQuantity"></param>
    /// <param name="lineTotal"></param>
    /// <param name="receiveQuantity"></param>
    /// <param name="rejectedQuantity"></param>
    /// <param name="stockedQuantity"></param>
    /// <param name="modifiedDateTime"></param>
    public PurchaseOrderDetail(
        PurchaseOrderId purchaseOrderId, 
        PurchaseOrderDetailId purchaseOrderDetailId,
        Date dueDate,
        Quantity orderQuantity, 
        ProductId productId, 
        Dollar unitPrice, 
        Dollar lineTotal, 
        decimal receiveQuantity, 
        decimal rejectedQuantity, 
        decimal stockedQuantity, 
        ModifiedDateTime modifiedDateTime)
    {
        PurchaseOrderId = purchaseOrderId;
        PurchaseOrderDetailId = purchaseOrderDetailId;
        DueDate = dueDate;
        ProductId = productId;
        UnitPrice = unitPrice;
        OrderQuantity = orderQuantity;
        LineTotal = lineTotal;
        ReceiveQuantity = receiveQuantity;
        RejectedQuantity = rejectedQuantity;
        StockedQuantity = stockedQuantity;
        ModifiedDateTime = modifiedDateTime;
    }

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
            0,
            0,
            0,
            ModifiedDateTime.Unregistered);
    }

    public PurchaseOrderId PurchaseOrderId  { get; }
    public PurchaseOrderDetailId PurchaseOrderDetailId  { get; }
    public Date DueDate  { get; }
    public ProductId ProductId  { get; }
    public Dollar UnitPrice  { get; }
    public Quantity OrderQuantity { get; }
    public Dollar LineTotal  { get; }
    public decimal ReceiveQuantity  { get; }
    public decimal RejectedQuantity  { get; }
    public decimal StockedQuantity  { get; }
    public ModifiedDateTime ModifiedDateTime  { get; }

}