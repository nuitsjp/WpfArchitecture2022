using MagicOnion;

namespace AdventureWorks.Purchasing.MagicOnion.Client;

public interface IShipMethodRepositoryServer : IService<IShipMethodRepositoryServer>
{
    UnaryResult<IList<ShipMethod>> GetShipMethodsAsync();
}