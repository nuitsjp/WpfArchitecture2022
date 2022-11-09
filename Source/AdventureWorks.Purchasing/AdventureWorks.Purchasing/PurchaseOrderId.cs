namespace AdventureWorks.Purchasing;

public partial struct PurchaseOrderId
{
    public static readonly PurchaseOrderId Unregistered = new(int.MinValue);
}