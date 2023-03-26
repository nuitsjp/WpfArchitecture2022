using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IShipMethodRepositoryService : IService<IShipMethodRepositoryService>
{
    UnaryResult<IList<ShipMethod>> GetShipMethodsAsync();
}