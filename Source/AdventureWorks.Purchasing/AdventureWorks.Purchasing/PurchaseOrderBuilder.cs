using AdventureWorks.Purchasing.Production;

namespace AdventureWorks.Purchasing;

public class PurchaseOrderBuilder
{
    private readonly EmployeeId _employeeId;
    private readonly Vendor _vendor;
    private readonly ShipMethod _shipMethod;
    private readonly Date _orderDate;
    private readonly IList<(Production.Product Product, PurchaseOrderDetail PurchaseOrderDetail)> _details = new List<(Product, PurchaseOrderDetail)>();

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

    public void AddProduct(Product product, short quantity)
    {
        var vendorProduct = _vendor
            .VendorProducts
            .Single(x => x.ProductId == product.ProductId);
        _details.Add(
            (
                product,
                new PurchaseOrderDetail(
                    _orderDate + vendorProduct.AverageLeadTime,
                    product.ProductId,
                    vendorProduct.StandardPrice,
                    quantity)));
    }

    public PurchaseOrder Build()
    {
        Dollar subTotal = _details
            .Select(x => x.PurchaseOrderDetail.LineTotal)
            .Sum();
        Dollar taxAmount = subTotal * _vendor.TaxRate;

        Gram totalWeight = _details
            .Select(x => x.Product.Weight * x.PurchaseOrderDetail.OrderQuantity)
            .Sum();
        Dollar freight = _shipMethod.ShipRate * totalWeight;

        return new(
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