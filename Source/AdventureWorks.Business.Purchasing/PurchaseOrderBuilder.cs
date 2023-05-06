namespace AdventureWorks.Business.Purchasing;

/// <summary>
/// 発注ビルダー
/// </summary>
/// <remarks>
/// 発注にはベンダーや支払い方法エンティティを直接持たない為、発注オブジェクトの構築をこのクラスに含める。
/// そうしない場合、ビジネスロジックがViewModelなどに漏れ出てしまうため。
/// </remarks>
public class PurchaseOrderBuilder
{
    /// <summary>
    /// 従業員ID
    /// </summary>
    private readonly EmployeeId _employeeId;
    /// <summary>
    /// ベンダー＾
    /// </summary>
    private readonly Vendor _vendor;
    /// <summary>
    /// 支払い方法
    /// </summary>
    private readonly ShipMethod _shipMethod;
    /// <summary>
    /// 発注日
    /// </summary>
    private readonly Date _orderDate;
    /// <summary>
    /// 発注明細
    /// </summary>
    private readonly IList<(Product Product, PurchaseOrderDetail PurchaseOrderDetail)> _details = new List<(Product, PurchaseOrderDetail)>();

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="vendor"></param>
    /// <param name="shipMethod"></param>
    /// <param name="orderDate"></param>
    public PurchaseOrderBuilder(
        EmployeeId employeeId, 
        Vendor vendor, 
        ShipMethod shipMethod, 
        Date orderDate)
    {
        _employeeId = employeeId;
        _vendor = vendor;
        _shipMethod = shipMethod;
        _orderDate = orderDate;
    }

    /// <summary>
    /// 発注する商品を追加する。
    /// </summary>
    /// <param name="product"></param>
    /// <param name="quantity"></param>
    public void AddProduct(Product product, Quantity quantity)
    {
        var vendorProduct = _vendor
            .VendorProducts
            .Single(x => x.ProductId == product.ProductId);
        _details.Add(
            (
                product,
                PurchaseOrderDetail.NewOrderDetail(
                    _orderDate + vendorProduct.AverageLeadTime,
                    product.ProductId,
                    vendorProduct.StandardPrice,
                    quantity)));
    }

    /// <summary>
    /// 発注をビルドする。
    /// </summary>
    /// <returns></returns>
    public PurchaseOrder Build()
    {
        Dollar subTotal = _details
            .Sum(x => x.PurchaseOrderDetail.LineTotal);
        Dollar taxAmount = subTotal * _vendor.TaxRate;

        Gram totalWeight = _details
            .Sum(x => x.Product.Weight * x.PurchaseOrderDetail.OrderQuantity);
        Dollar freight = _shipMethod.ShipRate * totalWeight;

        return PurchaseOrder.NewOrder(
            _employeeId,
            _vendor.VendorId,
            _shipMethod.ShipMethodId,
            _orderDate,
            subTotal,
            taxAmount,
            freight,
            _details.Select(x => x.PurchaseOrderDetail).ToList()
        );
    }
}