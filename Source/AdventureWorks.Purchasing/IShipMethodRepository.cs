namespace AdventureWorks.Business.Purchasing;

public interface IShipMethodRepository
{
    Task<IList<ShipMethod>> GetShipMethodsAsync();
}