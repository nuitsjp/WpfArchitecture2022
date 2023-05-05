using Dapper;

namespace AdventureWorks.Business.Purchasing.SqlServer;

public class VendorRepository : IVendorRepository
{
    private readonly PurchasingDatabase _database;

    public VendorRepository(PurchasingDatabase database)
    {
        _database = database;
    }

    public async Task<Vendor> GetVendorByIdAsync(VendorId vendorId)
    {
        using var connection = _database.Open();

        var products = await connection
            .QueryAsync<VendorProduct>(@"
select
	ProductId,
	AverageLeadTime,
	StandardPrice,
	LastReceiptCost,
	MinOrderQuantity,
	MaxOrderQuantity,
	OnOrderQuantity,
	UnitMeasureCode,
	ModifiedDateTime
from
	Purchasing.vProductVendor
where
	VendorId = @VendorId",
                new
                {
                    VendorId = vendorId
                });

        var vendor = await connection
            .QuerySingleAsync(@"
select
	VendorId,
	AccountNumber,
	Name,
	CreditRating,
	IsPreferredVendor,
	IsActive,
	PurchasingWebServiceUrl,
	StateProvinceID,
    TaxRate,
	ModifiedDateTime
from
	Purchasing.vVendor
where
	VendorId = @VendorId",
                new
                {
                    VendorId = vendorId
                });

        return new Vendor(
            new VendorId(vendor.VendorId),
            new AccountNumber(vendor.AccountNumber),
            vendor.Name,
            (CreditRating) vendor.CreditRating,
            Convert.ToBoolean(vendor.IsPreferredVendor),
            Convert.ToBoolean(vendor.IsActive),
            vendor.PurchasingWebServiceUrl is null
                ? null
                : new Uri(vendor.PurchasingWebServiceUrl),
            new TaxRate(vendor.TaxRate),
            new ModifiedDateTime(vendor.ModifiedDateTime),
            products.ToList());
    }
}