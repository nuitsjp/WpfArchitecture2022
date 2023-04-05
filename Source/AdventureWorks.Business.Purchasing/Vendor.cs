namespace AdventureWorks.Business.Purchasing;

public class Vendor
{
    public Vendor(
        VendorId vendorId, 
        AccountNumber accountNumber, 
        string name, 
        CreditRating creditRating, 
        bool isPreferredVendor, 
        bool isActive, 
        Uri? purchasingWebServiceUrl, 
        TaxRate taxRate, 
        ModifiedDateTime modifiedDateTime, 
        IReadOnlyList<VendorProduct> vendorProducts)
    {
        VendorId = vendorId;
        AccountNumber = accountNumber;
        Name = name;
        CreditRating = creditRating;
        IsPreferredVendor = isPreferredVendor;
        IsActive = isActive;
        PurchasingWebServiceUrl = purchasingWebServiceUrl;
        TaxRate = taxRate;
        ModifiedDateTime = modifiedDateTime;
        VendorProducts = vendorProducts;
    }

    public VendorId VendorId { get; }
    public AccountNumber AccountNumber { get; }
    public string Name { get; }
    public CreditRating CreditRating { get; }
    public bool IsPreferredVendor { get; }
    public bool IsActive { get; }
    public Uri? PurchasingWebServiceUrl { get; }
    public TaxRate TaxRate { get; }
    public ModifiedDateTime ModifiedDateTime { get; }
    public IReadOnlyList<VendorProduct> VendorProducts { get; }
}