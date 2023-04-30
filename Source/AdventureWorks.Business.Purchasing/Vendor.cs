namespace AdventureWorks.Business.Purchasing;

public record Vendor(
    VendorId VendorId, 
    AccountNumber AccountNumber, 
    string Name, 
    CreditRating CreditRating, 
    bool IsPreferredVendor, 
    bool IsActive, 
    Uri? PurchasingWebServiceUrl, 
    TaxRate TaxRate, 
    ModifiedDateTime ModifiedDateTime, 
    IReadOnlyList<VendorProduct> VendorProducts);