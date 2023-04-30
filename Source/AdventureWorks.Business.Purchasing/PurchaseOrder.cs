namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// 発注
/// </summary>
/// <param name="Id"></param>
/// <param name="RevisionNumber"></param>
/// <param name="Status"></param>
/// <param name="EmployeeId"></param>
/// <param name="VendorId"></param>
/// <param name="ShipMethodId"></param>
/// <param name="OrderDate"></param>
/// <param name="ShipDate"></param>
/// <param name="SubTotal"></param>
/// <param name="TaxAmount"></param>
/// <param name="Freight"></param>
/// <param name="TotalDue"></param>
/// <param name="ModifiedDateTime"></param>
/// <param name="Details"></param>
public record PurchaseOrder(
        PurchaseOrderId Id, 
        RevisionNumber RevisionNumber, 
        OrderStatus Status, 
        EmployeeId EmployeeId, 
        VendorId VendorId, 
        ShipMethodId ShipMethodId,
        Date OrderDate,
        Date? ShipDate, 
        Dollar SubTotal, 
        Dollar TaxAmount, 
        Dollar Freight, 
        Dollar TotalDue, 
        ModifiedDateTime ModifiedDateTime, 
        IReadOnlyList<PurchaseOrderDetail> Details)
{

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
}