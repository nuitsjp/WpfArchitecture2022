using AdventureWorks.Business;
using AdventureWorks.Business.Purchasing;

namespace AdventureWorks.Purchasing.App.Driver.Scenario.RePurchasingTest.再発注する.Purchasing;

public class ShipMethodRepository : IShipMethodRepository
{
    public async Task<IList<ShipMethod>> GetShipMethodsAsync()
    {
        await Task.CompletedTask;
        return new List<ShipMethod>
        {
            new(
                new ShipMethodId(1),
                "Method A",
                new Dollar(1),
                new DollarPerGram(100),
                new ModifiedDateTime(DateTime.Today)),
            new(
                new ShipMethodId(2),
                "Method B",
                new Dollar(2),
                new DollarPerGram(200),
                new ModifiedDateTime(DateTime.Today))
        };
    }
}