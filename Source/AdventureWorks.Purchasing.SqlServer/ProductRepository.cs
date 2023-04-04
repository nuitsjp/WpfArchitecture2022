using Dapper;

namespace AdventureWorks.Purchasing.SqlServer;

public class ProductRepository : IProductRepository
{
    private readonly PurchasingDatabase _database;

    public ProductRepository(PurchasingDatabase database)
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
            new { ProductId = productId });

    }
}