using Dapper;

namespace AdventureWorks.Purchasing.SqlServer;

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
	ProductID,
	AverageLeadTime,
	StandardPrice,
	LastReceiptCost,
	MinOrderQty as MinOrderQuantity,
	MaxOrderQty as MaxOrderQuantity,
	OnOrderQty as OnOrderQuantity,
	UnitMeasureCode,
	ModifiedDate as ModifiedDateTime
from
	Purchasing.ProductVendor
where
	BusinessEntityID = @VendorId",
                new
                {
                    VendorId = vendorId
                });

        var vendor = await connection
            .QuerySingleAsync(@"
select
	Vendor.BusinessEntityID as VendorId,
	Vendor.AccountNumber,
	Vendor.Name,
	Vendor.CreditRating,
	Vendor.PreferredVendorStatus as IsPreferredVendor,
	Vendor.ActiveFlag as IsActive,
	Vendor.PurchasingWebServiceURL as PurchasingWebServiceUrl,
	StateProvince.StateProvinceID,
    -- 税率が設定されていないデータがあるが、おそらくデータ不備のため一律10%を適用する
	case
		when SalesTaxRate.TaxRate is null then 10.0
		else SalesTaxRate.TaxRate
	end as TaxRate,
	Vendor.ModifiedDate as ModifiedDateTime
from
	Purchasing.Vendor
	inner join Person.BusinessEntityAddress
		on	Vendor.BusinessEntityID = BusinessEntityAddress.BusinessEntityID
	inner join Person.Address
		on	BusinessEntityAddress.AddressID = Address.AddressID
	inner join Person.StateProvince
		on	Address.StateProvinceID = StateProvince.StateProvinceID
	left outer join Sales.SalesTaxRate
		on	StateProvince.StateProvinceID = SalesTaxRate.StateProvinceID
where
	Vendor.BusinessEntityID = @VendorId",
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