using AdventureWorks.Database;
using AdventureWorks.Purchasing.Production;
using Dapper;

namespace AdventureWorks.Purchasing.SqlServer.Production;

public class ProductRepository : IProductRepository
{
    private readonly IDatabase _database;

    public ProductRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<Product> GetProductByIdAsync(ProductId productId)
    {
        using var connection = _database.Open();

        return await connection.QuerySingleAsync<Product>(@"
select
	ProductID,
	Name,
	ProductNumber,
	ISNULL(Color, '') as Color,
	StandardCost as StandardPrice,
	ListPrice,
	ISNULL(Weight, 0) as Weight,
	ModifiedDate as ModifiedDateTime
from
	Production.Product
where
    ProductID = @ProductId",
            new {ProductId = productId});

    }
}