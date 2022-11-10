namespace AdventureWorks;

public partial struct ModifiedDateTime
{
    public static readonly ModifiedDateTime Unregistered = new(DateTime.MinValue);
}

public class SalesTaxRate
{
    public SalesTaxRateId Id { get; }
    public string Name { get; }
    public TaxType TaxType { get; }

}

public enum TaxType
{
    /// <summary>
    /// Tax applied to retail transactions
    /// </summary>
    Retail = 1,

    /// <summary>
    /// Tax applied to wholesale transactions
    /// </summary>
    Wholesale = 2,

    /// <summary>
    /// Tax applied to all sales (retail and wholesale) transactions
    /// </summary>
    All = 3
}