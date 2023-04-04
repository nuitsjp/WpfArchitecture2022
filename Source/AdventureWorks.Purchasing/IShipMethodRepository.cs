namespace AdventureWorks.Purchasing;

public interface IShipMethodRepository
{
    Task<IList<ShipMethod>> GetShipMethodsAsync();
}