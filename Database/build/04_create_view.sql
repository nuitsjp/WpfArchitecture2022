use AdventureWorks;
go

--------------------------------------------------------------------------------------
-- AdventureWorks
--------------------------------------------------------------------------------------
drop view if exists AdventureWorks.vUser
go

create view 
	AdventureWorks.vUser
as
select
	BusinessEntityID as EmployeeId,
	LoginID as LoginId
from
	HumanResources.Employee;
go

--------------------------------------------------------------------------------------
-- 製品別標準購入先ベンダー
-- 不定期不定量発注方式に利用するリードタイムと在庫日数を持つ
--------------------------------------------------------------------------------------
drop view if exists RePurchasing.vStandardProductVendor
go

create view RePurchasing.vStandardProductVendor 
as
select
	RankedProductVendor.ProductID,
	RankedProductVendor.BusinessEntityID,
	AverageLeadTime,
	RePurchasing.GetInventoryDays(AverageLeadTime) as InventoryDays,
	StandardPrice,
	LastReceiptCost,
	LastReceiptDate,
	MinOrderQty,
	MaxOrderQty,
	isnull(OnOrderQty, 0) as OnOrderQty,
	UnitMeasureCode,
	ModifiedDate
from
	(
		select
			ROW_NUMBER() OVER (
				PARTITION BY 
					ProductVendor.ProductId 
				ORDER BY 
					Vendor.PreferredVendorStatus DESC,
					ProductVendor.StandardPrice
			) AS Rank,
			ProductVendor.ProductID,
			ProductVendor.BusinessEntityID,
			ProductVendor.AverageLeadTime,
			ProductVendor.StandardPrice,
			ProductVendor.LastReceiptCost,
			ProductVendor.LastReceiptDate,
			ProductVendor.MinOrderQty,
			ProductVendor.MaxOrderQty,
			ProductVendor.OnOrderQty,
			ProductVendor.UnitMeasureCode,
			ProductVendor.ModifiedDate
		from
			Purchasing.ProductVendor
			inner join Purchasing.Vendor
				on	ProductVendor.BusinessEntityID = Vendor.BusinessEntityID
	) as RankedProductVendor
where
	Rank = 1
go

--------------------------------------------------------------------------------------
-- すべての製品の製品別在庫
--------------------------------------------------------------------------------------
drop view if exists RePurchasing.vProductInventory
go

create view RePurchasing.vProductInventory as
select
	Product.ProductID,
	isnull(sum(ProductInventory.Quantity), 0) as Quantity
from
	Production.Product
	left outer join Production.ProductInventory
		on Product.ProductID = ProductInventory.ProductID
group by
	Product.ProductID
go

--------------------------------------------------------------------------------------
-- すべての製品の製品別未受領数
-- 発注しているがまだ未受領の数量
--------------------------------------------------------------------------------------
drop view if exists RePurchasing.vProductUnclaimedPurchase
go

create view RePurchasing.vProductUnclaimedPurchase as
select
	Product.ProductID,
	isnull(convert(int, sum(OrderQty)), 0) as Quantity
from	
	-- 販売データが2014年6月までしかないのに対して、発注関連はそれ以降も長期にわたって存在している
	-- おそらくテストデータが適切ではない部分があるが、ここでは発注が過去日かつ受領日が未来のものを集計する
	Production.Product
	left outer join Purchasing.PurchaseOrderDetail
		on	Product.ProductID = PurchaseOrderDetail.ProductID
		and dbo.GetToday() < PurchaseOrderDetail.DueDate
	left outer join Purchasing.PurchaseOrderHeader
		on	PurchaseOrderDetail.PurchaseOrderID = PurchaseOrderHeader.PurchaseOrderID
		and PurchaseOrderHeader.OrderDate < dbo.GetToday()
group by
	Product.ProductID
go

--------------------------------------------------------------------------------------
-- 販売実績のある製品の、製品別の１日当たり平均出荷量（不定期不定量発注方式の用語）
-- 実際には販売量を利用する
--------------------------------------------------------------------------------------
drop view if exists RePurchasing.vProductAverageDailyShipment
go

create view RePurchasing.vProductAverageDailyShipment as
select
	SpecialOfferProduct.ProductID,
	convert(float, sum(SalesOrderDetail.OrderQty)) / RePurchasing.GetAverageDailyShipmentsPeriodDays() as Quantity
from
	Sales.SpecialOfferProduct
	inner join Sales.SalesOrderDetail
		on	SpecialOfferProduct.ProductID = SalesOrderDetail.ProductID
		and	SpecialOfferProduct.SpecialOfferID = SalesOrderDetail.SpecialOfferID
	inner join Sales.SalesOrderHeader
		on	SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
where
	SalesOrderHeader.OrderDate between DATEADD(DAY, RePurchasing.GetAverageDailyShipmentsPeriodDays() * -1, dbo.GetToday()) and dbo.GetToday()
group by
	SpecialOfferProduct.ProductID
go

--------------------------------------------------------------------------------------
-- 販売実績のある製品の、製品別の出荷対応日数（不定期不定量発注方式の用語）
--------------------------------------------------------------------------------------
drop view if exists RePurchasing.vProductShipmentResponseDays
go

create view RePurchasing.vProductShipmentResponseDays
as
select
	-- 製品ID
	vProductAverageDailyShipment.ProductID,
	-- 出荷対応日数
	convert(int, FLOOR((vProductInventory.Quantity + vProductUnclaimedPurchase.Quantity) / vProductAverageDailyShipment.Quantity)) as ShipmentResponseDays,
	-- 在庫数
	vProductInventory.Quantity as InventoryQuantity,
	-- 未受領数
	vProductUnclaimedPurchase.Quantity as UnclaimedPurchaseQuantity,
	-- １日当たり平均出荷量
	vProductAverageDailyShipment.Quantity as AverageDailyShipmentQuantity
from
	RePurchasing.vProductAverageDailyShipment
	inner join RePurchasing.vProductInventory
		on	vProductAverageDailyShipment.ProductID = vProductInventory.ProductID
	inner join RePurchasing.vProductUnclaimedPurchase
		on	vProductAverageDailyShipment.ProductID = vProductUnclaimedPurchase.ProductID
go

--------------------------------------------------------------------------------------
-- 要購入製品
--------------------------------------------------------------------------------------
drop view if exists RePurchasing.vProductRequiringPurchase
go

create view RePurchasing.vProductRequiringPurchase as
select
	-- 発注先ベンダーID
	vStandardProductVendor.BusinessEntityID as VendorID,
	-- 発注先ベンダー名
	Vendor.Name as VendorName,
	-- プロダクトカテゴリーID
	ProductCategory.ProductCategoryID,
	-- プロダクトカテゴリー名
	ProductCategory.Name as ProductCategoryName,
	-- プロダクトサブカテゴリーID
	ProductSubcategory.ProductSubcategoryID,
	-- プロダクトサブカテゴリー名
	ProductSubcategory.Name as ProductSubcategoryName,
	-- プロダクトID
	Product.ProductID,
	-- プロダクト名
	Product.Name as ProductName,
	-- 発注数
	convert(int, ceiling(vStandardProductVendor.InventoryDays * vProductShipmentResponseDays.AverageDailyShipmentQuantity)) as PurchasingQuantity,
	-- 標準単価
	vStandardProductVendor.StandardPrice as UnitPrice,
	-- 出荷対応日数
	vProductShipmentResponseDays.ShipmentResponseDays,
	-- 平均リードタイム[日]
	vStandardProductVendor.AverageLeadTime,
	-- 在庫数
	vProductShipmentResponseDays.InventoryQuantity,
	-- 未受領数
	vProductShipmentResponseDays.UnclaimedPurchaseQuantity,
	-- １日当たり平均出荷量
	vProductShipmentResponseDays.AverageDailyShipmentQuantity
from
	-- 製品
	Production.Product
	-- 製品別標準購入先ベンダー
	inner join RePurchasing.vStandardProductVendor
		on	Product.ProductID = vStandardProductVendor.ProductID
	-- 製品別の出荷対応日数
	inner join RePurchasing.vProductShipmentResponseDays
		on	Product.ProductID = vProductShipmentResponseDays.ProductID
	-- 製品サブカテゴリー
	inner join Production.ProductSubcategory
		on	Product.ProductSubcategoryID = ProductSubcategory.ProductSubcategoryID
	-- 製品カテゴリー
	inner join Production.ProductCategory
		on	ProductSubcategory.ProductCategoryID = ProductCategory.ProductCategoryID
	-- 製品ベンダー
	inner join Purchasing.Vendor
		on	vStandardProductVendor.BusinessEntityID = Vendor.BusinessEntityID
where
	Product.MakeFlag = 0
	and vProductShipmentResponseDays.ShipmentResponseDays < vStandardProductVendor.AverageLeadTime
go

--------------------------------------------------------------------------------------
-- Serilog
-- ログの出力先と、出力設定に関するオブジェクト
--------------------------------------------------------------------------------------

-- View
create view 
	Serilog.vLogSettings 
as
select 
	ApplicationName, 
	MinimumLevel,
	Settings
from 
	Serilog.LogSettings;
go

--------------------------------------------------------------------------------------
-- 構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
CHECKPOINT;
GO
