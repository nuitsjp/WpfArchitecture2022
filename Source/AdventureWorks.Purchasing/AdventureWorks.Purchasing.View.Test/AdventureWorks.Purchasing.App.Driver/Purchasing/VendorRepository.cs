using AdventureWorks.Purchasing.Production;

namespace AdventureWorks.Purchasing.App.Driver.Purchasing;

public class VendorRepository : IVendorRepository
{
    public static List<Vendor> Vendors { get; } =
        Enumerable.Range(1, 3)
            .Select(vendor =>
                new Vendor(
                    new VendorId(vendor),
                    new AccountNumber($"Account {vendor}"),
                    $"Vendor {vendor}",
                    CreditRating.Superior,
                    vendor % 2 == 0,
                    vendor % 2 == 0,
                    new Uri("https://google.com"),
                    new TaxRate(vendor),
                    new ModifiedDateTime(DateTime.Today),
                    Enumerable.Range(1, vendor)
                        .Select(product =>
                            new VendorProduct(
                                new ProductId(vendor + 100 + product * 10 + 1),
                                new Days(vendor + 100 + product * 10 + 2),
                                new Dollar(vendor + 100 + product * 10 + 3),
                                new Dollar(vendor + 100 + product * 10 + 4),
                                new Quantity(vendor + 100 + product * 10 + 5),
                                new Quantity(vendor + 100 + product * 10 + 6),
                                new Quantity(vendor + 100 + product * 10 + 7),
                                new UnitMeasureCode($"measure{vendor + 100 + product * 10}"),
                                new ModifiedDateTime(DateTime.Today)))
                        .ToList())
            )
            .ToList();

    public async Task<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        await Task.CompletedTask;
        return Vendors.Single(x => x.VendorId == vendorId);
    }
}