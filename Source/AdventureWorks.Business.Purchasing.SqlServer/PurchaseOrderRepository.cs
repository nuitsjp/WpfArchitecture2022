using Dapper;

namespace AdventureWorks.Business.Purchasing.SqlServer;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
	private readonly PurchasingDatabase _database;

    public PurchaseOrderRepository(PurchasingDatabase database)
    {
        _database = database;
    }

    public async Task RegisterAsync(PurchaseOrder purchaseOrder)
    {
        #region Query
        const string headerQuery = @"
insert into 
	Purchasing.vPurchaseOrder
(
	EmployeeID,
	VendorID,
	ShipMethodID,
	ShipDate,
	SubTotal,
	TaxAmt,
	Freight
) values (
	@EmployeeId,
	@VendorId,
	@ShipMethodId,
	@ShipDate,
	@SubTotal,
	@TaxAmount,
	@Freight
);

select SCOPE_IDENTITY();
";
        const string detailQuery = @"
insert into
    Purchasing.vPurchaseOrderDetail
(
	PurchaseOrderID,
	DueDate,
	OrderQty,
	ProductID,
	UnitPrice,
	ReceivedQty,
	RejectedQty
) values (
	@PurchaseOrderId,
	@DueDate,
	@OrderQuantity,
	@ProductId,
	@UnitPrice,
	@ReceiveQuantity,
	@RejectedQuantity
)
";
        #endregion

        using var transaction = _database.BeginTransaction();

        var orderId = await transaction.Connection.ExecuteScalarAsync<int>(headerQuery, purchaseOrder);

        foreach (var detail in purchaseOrder.Details)
        {
            await transaction.Connection.ExecuteAsync(
                detailQuery, 
                new
                {
                    PurchaseOrderId = orderId,
					detail.DueDate,
					detail.OrderQuantity,
					detail.ProductId,
					detail.UnitPrice,
					detail.ReceiveQuantity,
					detail.RejectedQuantity
                });
        }

		transaction.Complete();
    }
}