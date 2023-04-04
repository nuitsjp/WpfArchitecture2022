namespace AdventureWorks.Purchasing.App.Driver.Purchasing;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    public Task RegisterAsync(PurchaseOrder purchaseOrder)
    {
        var vendor =
            VendorRepository
                .Vendors
                .Single(x => x.VendorId == purchaseOrder.VendorId);
        VendorRepository.Vendors.Remove(vendor);
        return Task.CompletedTask;
    }
}