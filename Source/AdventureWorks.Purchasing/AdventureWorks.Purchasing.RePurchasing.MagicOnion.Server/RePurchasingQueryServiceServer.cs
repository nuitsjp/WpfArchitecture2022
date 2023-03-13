using AdventureWorks.Purchasing.RePurchasing.MagicOnion.Client;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.RePurchasing.MagicOnion.Server;

/// <remarks>
/// アセンブリーを解析してサービス登録するため、参照している箇所が無いように見えるが実際には利用されているため
/// 警告を抑制する。
/// </remarks>
// ReSharper disable once UnusedMember.Global
public class RePurchasingQueryServiceServer : ServiceBase<IRePurchasingQueryServiceServer>, IRePurchasingQueryServiceServer
{
    private readonly IRePurchasingQueryService _service;
    public RePurchasingQueryServiceServer(IRePurchasingQueryService service)
    {
        _service = service;
    }

    public async UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        try
        {
            return await _service.GetRequiringPurchaseProductsAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}