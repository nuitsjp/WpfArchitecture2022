using MagicOnion;

namespace AdventureWorks.Business.Purchasing.MagicOnion;

public interface IShipMethodRepositoryService : IService<IShipMethodRepositoryService>
{
    UnaryResult<IList<ShipMethod>> GetShipMethodsAsync();
}