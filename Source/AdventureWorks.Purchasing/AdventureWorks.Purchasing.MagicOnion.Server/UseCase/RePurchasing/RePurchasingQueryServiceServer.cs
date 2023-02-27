using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.MagicOnion.Server.UseCase.RePurchasing;

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