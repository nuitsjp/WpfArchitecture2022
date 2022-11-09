using UnitGenerator;

namespace AdventureWorks.Purchasing
{

    /// <summary>
    /// ID of Vendor
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct VendorId
    {
    }

    /// <summary>
    /// ID of PurchaseOrder
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct PurchaseOrderId
    {
    }

    /// <summary>
    /// ID of PurchaseOrderDetail
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct PurchaseOrderDetailId
    {
    }

    /// <summary>
    /// ID of ShipMethod
    /// </summary>
    [UnitOf(typeof(int), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct ShipMethodId
    {
    }

    /// <summary>
    /// Vendor account (identification) number.
    /// </summary>
    [UnitOf(typeof(string), UnitGenerateOptions.DapperTypeHandler)]
    public partial struct AccountNumber
    {
    }

}
