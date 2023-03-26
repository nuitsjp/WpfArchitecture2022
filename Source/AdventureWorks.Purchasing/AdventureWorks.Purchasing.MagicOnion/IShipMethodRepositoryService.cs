using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion;

public interface IShipMethodRepositoryService : IService<IShipMethodRepositoryService>
{
    UnaryResult<IList<ShipMethod>> GetShipMethodsAsync();
}