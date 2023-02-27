
using AdventureWorks.Database;
using Dapper;

namespace AdventureWorks.Purchasing.Database;

public class ShipMethodRepository : IShipMethodRepository
{
    private readonly IDatabase _database;

    public ShipMethodRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<IList<ShipMethod>> GetShipMethodsAsync()
    {
        using var connection = _database.Open();

        return (await connection.QueryAsync<ShipMethod>(@"
select
	ShipMethodID as ShipMethodId,
	Name,
	ShipBase,
	ShipRate,
	ModifiedDate as ModifiedDateTime
from
	Purchasing.ShipMethod"))
            .ToList();
    }
}