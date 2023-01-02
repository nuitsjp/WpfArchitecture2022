using UnitGenerator;

namespace AdventureWorks.Purchasing
{
    /// <summary>
    /// Vendor account (identification) number.
    /// </summary>
    [UnitOf(typeof(string), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct AccountNumber
    {
    }

}
