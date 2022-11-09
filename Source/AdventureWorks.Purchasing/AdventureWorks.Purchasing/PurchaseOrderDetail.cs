using AdventureWorks.Production;

namespace AdventureWorks.Purchasing;

public record PurchaseOrderDetail
{
    /// <summary>
    /// 既存の発注明細をインスタンス化する。
    /// </summary>
    /// <param name="purchaseOrderId"></param>
    /// <param name="purchaseOrderDetailId"></param>
    /// <param name="dueDate"></param>
    /// <param name="orderQuantity"></param>
    /// <param name="productId"></param>
    /// <param name="unitPrice"></param>
    /// <param name="lineTotal"></param>
    /// <param name="receiveQuantity"></param>
    /// <param name="rejectedQuantity"></param>
    /// <param name="stockedQuantity"></param>
    /// <param name="modifiedDateTime"></param>
    public PurchaseOrderDetail(
        PurchaseOrderId purchaseOrderId, 
        PurchaseOrderDetailId purchaseOrderDetailId, 
        DateTime dueDate, 
        short orderQuantity, 
        ProductId productId, 
        Money unitPrice, 
        Money lineTotal, 
        decimal receiveQuantity, 
        decimal rejectedQuantity, 
        decimal stockedQuantity, 
        ModifiedDateTime modifiedDateTime)
    {
        PurchaseOrderId = purchaseOrderId;
        PurchaseOrderDetailId = purchaseOrderDetailId;
        DueDate = dueDate;
        OrderQuantity = orderQuantity;
        ProductId = productId;
        UnitPrice = unitPrice;
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
    /// <param name="orderQuantity"></param>
    /// <param name="productId"></param>
    /// <param name="unitPrice"></param>
    /// <param name="lineTotal"></param>
    /// <param name="receiveQuantity"></param>
    /// <param name="rejectedQuantity"></param>
    /// <param name="stockedQuantity"></param>
    /// <param name="modifiedDateTime"></param>
    public PurchaseOrderDetail(
        DateTime dueDate,
        short orderQuantity,
        ProductId productId,
        Money unitPrice,
        Money lineTotal,
        decimal receiveQuantity,
        decimal rejectedQuantity,
        decimal stockedQuantity,
        ModifiedDateTime modifiedDateTime) :
        this(
            PurchaseOrderId.Unregistered,
            PurchaseOrderDetailId.Unregistered,
            dueDate,
            orderQuantity,
            productId,
            unitPrice,
            lineTotal,
            receiveQuantity,
            rejectedQuantity,
            stockedQuantity,
            modifiedDateTime)
    {
    }

    public PurchaseOrderId PurchaseOrderId  { get; }
    public PurchaseOrderDetailId PurchaseOrderDetailId  { get; }
    public DateTime DueDate  { get; }
    public short OrderQuantity  { get; }
    public ProductId ProductId  { get; }
    public Money UnitPrice  { get; }
    public Money LineTotal  { get; }
    public decimal ReceiveQuantity  { get; }
    public decimal RejectedQuantity  { get; }
    public decimal StockedQuantity  { get; }
    public ModifiedDateTime ModifiedDateTime  { get; }
}