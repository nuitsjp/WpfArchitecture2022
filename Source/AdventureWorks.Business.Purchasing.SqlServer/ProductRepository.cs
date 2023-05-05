using Dapper;

namespace AdventureWorks.Business.Purchasing.SqlServer;

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
	ProductId,
	Name,
	ProductNumber,
	Color,
	StandardPrice,
	ListPrice,
	Weight,
	ModifiedDateTime
from
	Purchasing.vProduct
where
    ProductID = @ProductId",
            new { ProductId = productId });

    }
}