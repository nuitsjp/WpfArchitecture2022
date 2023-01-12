namespace AdventureWorks.Purchasing;

public interface IShipMethodRepository
{
    Task<IEnumerable<ShipMethod>> GetShipMethodsAsync();
}