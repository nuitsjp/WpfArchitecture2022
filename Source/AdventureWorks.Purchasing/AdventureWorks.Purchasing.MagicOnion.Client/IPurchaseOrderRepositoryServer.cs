using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IPurchaseOrderRepositoryServer : IService<IPurchaseOrderRepositoryServer>
{
    UnaryResult RegisterAsync(PurchaseOrder purchaseOrder);
}