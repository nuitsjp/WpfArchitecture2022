using UnitGenerator;

namespace AdventureWorks.Purchasing;

/// <summary>
/// ID of PurchaseOrder
/// </summary>
[UnitOf(typeof(int))]
public partial struct PurchaseOrderId
{
}

public partial struct PurchaseOrderId
{
    public static readonly PurchaseOrderId Unregistered = new(int.MinValue);
}