using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public interface IPurchaseOrderRepositoryService : IService<IPurchaseOrderRepositoryService>
{
    UnaryResult RegisterAsync(PurchaseOrder purchaseOrder);
}