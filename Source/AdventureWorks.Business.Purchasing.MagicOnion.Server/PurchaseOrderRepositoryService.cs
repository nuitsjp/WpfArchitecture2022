using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Business.Purchasing.MagicOnion.Server;

/// <summary>
/// 発注リポジトリーサービス
/// </summary>
/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class PurchaseOrderRepositoryService : ServiceBase<IPurchaseOrderRepositoryService> , IPurchaseOrderRepositoryService
{
    /// <summary>
    /// 発注リポジトリー
    /// </summary>
    private readonly IPurchaseOrderRepository _repository;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="repository"></param>
    public PurchaseOrderRepositoryService(IPurchaseOrderRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 発注する。
    /// </summary>
    /// <param name="purchaseOrder"></param>
    /// <returns></returns>
    public async UnaryResult RegisterAsync(PurchaseOrder purchaseOrder)
    {
        await _repository.RegisterAsync(purchaseOrder);
    }
}