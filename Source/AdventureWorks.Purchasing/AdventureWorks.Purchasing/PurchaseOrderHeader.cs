﻿using AdventureWorks.Production;

namespace AdventureWorks.Purchasing;

public class PurchaseOrderHeader
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
    public PurchaseOrderHeader(
        PurchaseOrderId id, 
        RevisionNumber revisionNumber, 
        OrderStatus status, 
        EmployeeId employeeId, 
        VendorId vendorId, 
        ShipMethodId shipMethodId, 
        DateTime orderDate, 
        DateTime? shipDate, 
        Money subTotal, 
        Money taxAmount, 
        Money freight, 
        Money totalDue, 
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
    public PurchaseOrderHeader(
        RevisionNumber revisionNumber,
        OrderStatus status,
        EmployeeId employeeId,
        VendorId vendorId,
        ShipMethodId shipMethodId,
        DateTime orderDate,
        DateTime? shipDate,
        Money subTotal,
        Money taxAmount,
        Money freight,
        Money totalDue,
        ModifiedDateTime modifiedDateTime, 
        IReadOnlyList<PurchaseOrderDetail> details) :
        this(
            PurchaseOrderId.Unregistered,
            revisionNumber, 
            status, 
            employeeId, 
            vendorId, 
            shipMethodId, 
            orderDate, 
            shipDate, 
            subTotal, 
            taxAmount, 
            freight, 
            totalDue, 
            modifiedDateTime, 
            details)
    {
    }

    public PurchaseOrderId Id { get; }
    public RevisionNumber RevisionNumber { get; }
    public OrderStatus Status  { get; }
    public EmployeeId EmployeeId { get; }
    public VendorId VendorId { get; }
    public ShipMethodId ShipMethodId { get; }
    public DateTime OrderDate { get; }
    public DateTime? ShipDate { get; }
    public Money SubTotal { get; }
    public Money TaxAmount { get; }
    public Money Freight { get; }
    public Money TotalDue { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
    public IReadOnlyList<PurchaseOrderDetail> Details { get; }

}
