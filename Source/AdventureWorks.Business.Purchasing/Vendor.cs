namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// ベンダー
/// </summary>
/// <param name="VendorId"></param>
/// <param name="AccountNumber"></param>
/// <param name="Name"></param>
/// <param name="CreditRating"></param>
/// <param name="IsPreferredVendor"></param>
/// <param name="IsActive"></param>
/// <param name="PurchasingWebServiceUrl"></param>
/// <param name="TaxRate"></param>
/// <param name="ModifiedDateTime"></param>
/// <param name="VendorProducts"></param>
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