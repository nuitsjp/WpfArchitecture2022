using AdventureWorks.Database;
using Dapper;

namespace AdventureWorks.Purchasing.SqlServer;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
	private readonly IDatabase _database;

    public PurchaseOrderRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task RegisterAsync(PurchaseOrder purchaseOrder)
    {
        #region Query
        const string headerQuery = @"
INSERT INTO 
	Purchasing.PurchaseOrderHeader
(
	EmployeeID,
	VendorID,
	ShipMethodID,
	ShipDate,
	SubTotal,
	TaxAmt,
	Freight
) VALUES (
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
INSERT INTO Purchasing.PurchaseOrderDetail
(
	PurchaseOrderID,
	DueDate,
	OrderQty,
	ProductID,
	UnitPrice,
	ReceivedQty,
	RejectedQty
) VALUES (
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