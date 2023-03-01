namespace AdventureWorks.Purchasing;

public class PurchaseOrder
{
    /// <summary>
    /// 既存の発注をインスタンス化する。
    /// </summary>
    /// <param name="id"></param>
    /// <param name="revisionNumber"></param>
    /// <param name="status"></param>
    /// <param name="employeeId"></param>
    /// <param name="vendorId"></param>
    /// <param name="shipMethodId"></param>
    /// <param name="orderDate"></param>
    /// <param name="shipDate"></param>
    /// <param name="subTotal"></param>
    /// <param name="taxAmount"></param>
    /// <param name="freight"></param>
    /// <param name="totalDue"></param>
    /// <param name="modifiedDateTime"></param>
    /// <param name="details"></param>
    public PurchaseOrder(
        PurchaseOrderId id, 
        RevisionNumber revisionNumber, 
        OrderStatus status, 
        EmployeeId employeeId, 
        VendorId vendorId, 
        ShipMethodId shipMethodId,
        Date orderDate,
        Date? shipDate, 
        Dollar subTotal, 
        Dollar taxAmount, 
        Dollar freight, 
        Dollar totalDue, 
        ModifiedDateTime modifiedDateTime, 
        IReadOnlyList<PurchaseOrderDetail> details)
    {
        Id = id;
        RevisionNumber = revisionNumber;
        Status = status;
        EmployeeId = employeeId;
        VendorId = vendorId;
        ShipMethodId = shipMethodId;
        OrderDate = orderDate;
        ShipDate = shipDate;
        SubTotal = subTotal;
        TaxAmount = taxAmount;
        Freight = freight;
        TotalDue = totalDue;
        ModifiedDateTime = modifiedDateTime;
        Details = details;
    }

    /// <summary>
    /// 新規の発注をインスタンス化する。
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="vendorId"></param>
    /// <param name="shipMethodId"></param>
    /// <param name="orderDate"></param>
    /// <param name="subTotal"></param>
    /// <param name="taxAmount"></param>
    /// <param name="freight"></param>
    /// <param name="details"></param>
    public static PurchaseOrder NewOrder(
        EmployeeId employeeId,
        VendorId vendorId,
        ShipMethodId shipMethodId,
        Date orderDate,
        Dollar subTotal,
        Dollar taxAmount,
        Dollar freight,
        IReadOnlyList<PurchaseOrderDetail> details)
    {
        return new(
            PurchaseOrderId.Unregistered,
            RevisionNumber.Unregistered,
            OrderStatus.Pending,
            employeeId,
            vendorId,
            shipMethodId,
            orderDate,
            null,
            subTotal,
            taxAmount,
            freight,
            subTotal + taxAmount + freight,
            ModifiedDateTime.Unregistered,
            details);
    }

    public PurchaseOrderId Id { get; }
    public RevisionNumber RevisionNumber { get; }
    public OrderStatus Status  { get; }
    public EmployeeId EmployeeId { get; }
    public VendorId VendorId { get; }
    public ShipMethodId ShipMethodId { get; }
    public Date OrderDate { get; }
    public Date? ShipDate { get; }
    public Dollar SubTotal { get; }
    public Dollar TaxAmount { get; }
    public Dollar Freight { get; }
    public Dollar TotalDue { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
    public IReadOnlyList<PurchaseOrderDetail> Details { get; }

}