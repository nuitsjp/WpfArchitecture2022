using AdventureWorks.Purchasing.Production;
using AdventureWorks.Purchasing.UseCase.RePurchasing;

namespace AdventureWorks.Purchasing.App.Driver.Purchasing;

public class RePurchasingQueryService : IRePurchasingQueryService
{
    public async Task<IList<RequiringPurchaseProduct>> GetRequiringPurchaseProductsAsync()
    {
        await Task.CompletedTask;
        return VendorRepository
            .Vendors
            .SelectMany(vendor => 
                vendor.VendorProducts
                    .Select(product => 
                        new RequiringPurchaseProduct(
                            vendor.VendorId,
                            vendor.Name,
                            new ProductCategoryId(product.ProductId.AsPrimitive()),
                            $"Category {product.ProductId.AsPrimitive()}",
                            new ProductSubcategoryId(product.ProductId.AsPrimitive()),
                            $"Subcategory {product.ProductId.AsPrimitive()}",
                            product.ProductId,
                            $"Product {product.ProductId.AsPrimitive()}",
                            new Quantity(product.ProductId.AsPrimitive()),
                            product.StandardPrice,
                            new Days(product.ProductId.AsPrimitive()),
                            product.AverageLeadTime,
                            product.MinOrderQuantity,
                            product.MaxOrderQuantity,
                            new DoubleQuantity(product.ProductId.AsPrimitive())
                        ))
                )
            .ToList();
    }
}