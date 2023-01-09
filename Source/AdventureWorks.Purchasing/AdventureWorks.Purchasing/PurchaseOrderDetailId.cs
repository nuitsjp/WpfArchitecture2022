using UnitGenerator;

namespace AdventureWorks.Purchasing;

/// <summary>
/// ID of PurchaseOrderDetail
/// </summary>
[UnitOf(typeof(int))]
public partial struct PurchaseOrderDetailId
{
}

public partial struct PurchaseOrderDetailId
{
    public static readonly PurchaseOrderDetailId Unregistered = new(int.MinValue);
}