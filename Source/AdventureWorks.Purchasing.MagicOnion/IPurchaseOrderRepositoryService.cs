using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion;

public interface IPurchaseOrderRepositoryService : IService<IPurchaseOrderRepositoryService>
{
    UnaryResult RegisterAsync(PurchaseOrder purchaseOrder);
}