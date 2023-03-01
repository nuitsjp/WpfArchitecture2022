using AdventureWorks.Purchasing.MagicOnion.Client;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public class PurchaseOrderRepositoryServer : ServiceBase<IPurchaseOrderRepositoryServer> , IPurchaseOrderRepositoryServer
{
    private readonly IPurchaseOrderRepository _repository;

    public PurchaseOrderRepositoryServer(IPurchaseOrderRepository repository)
    {
        _repository = repository;
    }

    public async UnaryResult RegisterAsync(PurchaseOrder purchaseOrder)
    {
        await _repository.RegisterAsync(purchaseOrder);
    }
}