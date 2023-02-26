using AdventureWorks.Purchasing.UseCase.RePurchasing;
using AdventureWorks.Purchasing.UseCase.RePurchasing.Client;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public class RePurchasingQueryServiceServer : ServiceBase<IRePurchasingQueryServiceServer>, IRePurchasingQueryServiceServer
{
    private readonly IRePurchasingQueryService _service;
#pragma warning disable CS1998
    public RePurchasingQueryServiceServer(IRePurchasingQueryService service)
    {
        _service = service;
    }

    public async UnaryResult<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
#pragma warning restore CS1998
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