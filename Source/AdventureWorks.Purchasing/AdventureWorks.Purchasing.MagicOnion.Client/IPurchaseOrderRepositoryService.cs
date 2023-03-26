using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IPurchaseOrderRepositoryService : IService<IPurchaseOrderRepositoryService>
{
    UnaryResult RegisterAsync(PurchaseOrder purchaseOrder);
}