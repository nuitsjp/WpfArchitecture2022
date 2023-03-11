using AdventureWorks.Purchasing.Production;

namespace AdventureWorks.Purchasing.App.Driver.Purchasing;

public class ProductRepository : IProductRepository
{
    public async Task<Product> GetProductByIdAsync(ProductId productId)
    {
        await Task.CompletedTask;
        var vendorProduct = VendorRepository
            .Vendors
            .SelectMany(x => x.VendorProducts)
            .Single(x => x.ProductId == productId);

        return new Product(
            vendorProduct.ProductId,
            $"Product{productId.AsPrimitive()}",
            $"Number{productId.AsPrimitive()}",
            $"Color {productId.AsPrimitive()}",
            vendorProduct.StandardPrice,
            vendorProduct.LastReceiptCost,
            new Gram(vendorProduct.ProductId.AsPrimitive() * 100),
            new ModifiedDateTime(DateTime.Today)
        );
    }
}