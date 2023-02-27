using AdventureWorks.Purchasing.MagicOnion.Client;
using AdventureWorks.Purchasing.MagicOnion.Client.UseCase.RePurchasing;
using MagicOnion;
using MagicOnion.Server;

namespace AdventureWorks.Purchasing.MagicOnion.Server;

public class ShipMethodRepositoryServer : ServiceBase<IShipMethodRepositoryServer>, IShipMethodRepositoryServer
{
    private readonly IShipMethodRepository _repository;

    public ShipMethodRepositoryServer(IShipMethodRepository repository)
    {
        _repository = repository;
    }

    public async UnaryResult<IList<ShipMethod>> GetShipMethodsAsync()
    {
        return await _repository.GetShipMethodsAsync();
    }
}