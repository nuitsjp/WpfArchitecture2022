using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class PurchaseOrderRepositoryService : ServiceBase<IPurchaseOrderRepositoryService> , IPurchaseOrderRepositoryService
{
    private readonly IPurchaseOrderRepository _repository;

    public PurchaseOrderRepositoryService(IPurchaseOrderRepository repository)
    {
        _repository = repository;
    }

    public async UnaryResult RegisterAsync(PurchaseOrder purchaseOrder)
    {
        await _repository.RegisterAsync(purchaseOrder);
    }
}