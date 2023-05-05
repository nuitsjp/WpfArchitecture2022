using Dapper;

namespace AdventureWorks.Business.Purchasing.SqlServer;

public class ShipMethodRepository : IShipMethodRepository
{
    private readonly PurchasingDatabase _database;

    public ShipMethodRepository(PurchasingDatabase database)
    {
        _database = database;
    }

    public async Task<IList<ShipMethod>> GetShipMethodsAsync()
    {
        using var connection = _database.Open();

        return (await connection.QueryAsync<ShipMethod>(@"
select
	ShipMethodId,
	Name,
	ShipBase,
	ShipRate,
	ModifiedDateTime
from
	Purchasing.vShipMethod"))
            .ToList();
    }
}