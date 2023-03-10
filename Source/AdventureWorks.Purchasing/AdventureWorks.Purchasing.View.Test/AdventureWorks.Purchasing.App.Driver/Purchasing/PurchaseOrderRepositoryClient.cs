namespace AdventureWorks.Purchasing.App.Driver.Purchasing;

public class PurchaseOrderRepositoryClient : IPurchaseOrderRepository
{
    public Task RegisterAsync(PurchaseOrder purchaseOrder)
    {
        return Task.CompletedTask;
    }
}