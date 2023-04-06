use AdventureWorks;

--------------------------------------------------------------------------------------
-- Cleanup table and view
--------------------------------------------------------------------------------------
declare @drop_statements nvarchar(max) = '';

select 
	@drop_statements += 'drop ' + 
	case
        when type = 'U' then 'table '
        when type = 'V' then 'view '
	end + quotename(schema_name(schema_id)) + '.' + quotename(name) + ';' + char(13)
from 
	sys.objects
where 
	type in ('U', 'V') 
	and schema_name(schema_id) = 'Serilog';

execute sp_executesql @drop_statements;

--------------------------------------------------------------------------------------
-- Serilog
--------------------------------------------------------------------------------------

-- Login
if exists
    (select name from master.sys.server_principals where name = 'Serilog')
begin
	drop login Serilog
end

create login Serilog with password = 'RG3CbVP!2U4hT5';
go

-- User
drop user if exists Serilog
go
create user Serilog for login Serilog;
go

-- Schema
drop schema if exists Serilog
go

create schema Serilog
go

-- Table
create table [Serilog].[LogSettings](
    [ApplicationName] nvarchar(400) not null,
	[MinimumLevel] nvarchar(max) not null,
	constraint [PK_LogSettings] primary key ([ApplicationName])
)	on [PRIMARY];
go

create table [Serilog].[Logs](
	[Id] [int] identity(1,1) not null,
	[Message] [nvarchar](max) null,
	[Level] [nvarchar](max) null,
	[TimeStamp] [datetime] null,
	[Exception] [nvarchar](max) null,
	[ApplicationType] [nvarchar](max) null,
	[Application] [nvarchar](max) null,
	[MachineName] [nvarchar](max) null,
	[UserName] [nvarchar](max) null,
	[ProcessId] [int] null,
	[ThreadId] [int] null,
	[CorrelationId] [int] null,
	constraint [PK_Logs] primary key clustered ([Id] asc) 
		with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, optimize_for_sequential_key = off) on [PRIMARY]
)	on [PRIMARY] textimage_on [PRIMARY]
go

-- View
create view 
	[serilog].[vLogSettings] 
as
select 
	[ApplicationName], 
	[MinimumLevel]
from 
	[Serilog].[LogSettings];
go

-- Grant
grant select on [Serilog].[vLogSettings] to Serilog;

--------------------------------------------------------------------------------------
-- 「今日」のdatetimeを取得する。
-- getdate()から変換した場合、テストが困難になるためスカラー関数を利用する。
-- 
-- 特にAdventureWorksのサンプルが作成された日付から、実行時の日付を現在そてして扱うと
-- 不都合が多いため、「今日」を「2014-07-01」に固定する
--------------------------------------------------------------------------------------
drop function if exists GetToday
go

create function GetToday()
returns date
as
begin
	return convert(date, '2014-07-01')
end
go

--select dbo.GetToday() as Today
go

--------------------------------------------------------------------------------------
-- 平均リードタイムから、不定期不定量発注方式の在庫日数を取得する
--------------------------------------------------------------------------------------
drop function if exists Purchasing.GetInventoryDays
go

create function Purchasing.GetInventoryDays(
	@AverageLeadTime int
)
returns int
as
begin
	return @AverageLeadTime + GREATEST(4, CEILING(@AverageLeadTime * 0.2));
end
go

--select Purchasing.GetInventoryDays(10) as InventoryDays
go

--------------------------------------------------------------------------------------
-- 日別平均出荷数を求める期間の日数を取得する
--------------------------------------------------------------------------------------
drop function if exists Purchasing.GetAverageDailyShipmentsPeriodDays
go

create function Purchasing.GetAverageDailyShipmentsPeriodDays()
returns int
as
begin
	return 90;
end
go

--select Purchasing.GetAverageDailyShipmentsPeriodDays() as AverageDailyShipmentsPeriodDays
go

--------------------------------------------------------------------------------------
-- 製品別標準購入先ベンダー
-- 不定期不定量発注方式に利用するリードタイムと在庫日数を持つ
--------------------------------------------------------------------------------------
drop view if exists Purchasing.StandardProductVendor
go

create view Purchasing.StandardProductVendor 
as
select
	RankedProductVendor.ProductID,
	RankedProductVendor.BusinessEntityID,
	AverageLeadTime,
	Purchasing.GetInventoryDays(AverageLeadTime) as InventoryDays,
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

--select * from Purchasing.StandardProductVendor


--------------------------------------------------------------------------------------
-- すべての製品の製品別在庫
--------------------------------------------------------------------------------------
drop view if exists Purchasing.ProductInventory
go

create view Purchasing.ProductInventory as
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

--select * from Purchasing.ProductInventory
go

--------------------------------------------------------------------------------------
-- すべての製品の製品別未受領数
-- 発注しているがまだ未受領の数量
--------------------------------------------------------------------------------------
drop view if exists Purchasing.ProductUnclaimedPurchase
go

create view Purchasing.ProductUnclaimedPurchase as
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

--select * from Purchasing.ProductUnclaimedPurchase
go

--------------------------------------------------------------------------------------
-- 販売実績のある製品の、製品別の１日当たり平均出荷量（不定期不定量発注方式の用語）
-- 実際には販売量を利用する
--------------------------------------------------------------------------------------
drop view if exists Purchasing.ProductAverageDailyShipment
go

create view Purchasing.ProductAverageDailyShipment as
select
	SpecialOfferProduct.ProductID,
	convert(float, sum(SalesOrderDetail.OrderQty)) / Purchasing.GetAverageDailyShipmentsPeriodDays() as Quantity
from
	Sales.SpecialOfferProduct
	inner join Sales.SalesOrderDetail
		on	SpecialOfferProduct.ProductID = SalesOrderDetail.ProductID
		and	SpecialOfferProduct.SpecialOfferID = SalesOrderDetail.SpecialOfferID
	inner join Sales.SalesOrderHeader
		on	SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
where
	SalesOrderHeader.OrderDate between DATEADD(DAY, Purchasing.GetAverageDailyShipmentsPeriodDays() * -1, dbo.GetToday()) and dbo.GetToday()
group by
	SpecialOfferProduct.ProductID
go

--select * from Purchasing.ProductAverageDailyShipment
go

--------------------------------------------------------------------------------------
-- 販売実績のある製品の、製品別の出荷対応日数（不定期不定量発注方式の用語）
--------------------------------------------------------------------------------------
drop view if exists Purchasing.ProductShipmentResponseDays
go

create view Purchasing.ProductShipmentResponseDays
as
select
	-- 製品ID
	ProductAverageDailyShipment.ProductID,
	-- 出荷対応日数
	convert(int, FLOOR((ProductInventory.Quantity + ProductUnclaimedPurchase.Quantity) / ProductAverageDailyShipment.Quantity)) as ShipmentResponseDays,
	-- 在庫数
	ProductInventory.Quantity as InventoryQuantity,
	-- 未受領数
	ProductUnclaimedPurchase.Quantity as UnclaimedPurchaseQuantity,
	-- １日当たり平均出荷量
	ProductAverageDailyShipment.Quantity as AverageDailyShipmentQuantity
from
	Purchasing.ProductAverageDailyShipment
	inner join Purchasing.ProductInventory
		on	ProductAverageDailyShipment.ProductID = ProductInventory.ProductID
	inner join Purchasing.ProductUnclaimedPurchase
		on	ProductAverageDailyShipment.ProductID = ProductUnclaimedPurchase.ProductID
go

--select * from Purchasing.ProductShipmentResponseDays order by ShipmentResponseDays
go

--------------------------------------------------------------------------------------
-- 要購入製品
--------------------------------------------------------------------------------------
drop view if exists Purchasing.ProductRequiringPurchase
go

create view Purchasing.ProductRequiringPurchase as
select
	-- 発注先ベンダーID
	StandardProductVendor.BusinessEntityID as VendorID,
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
	convert(int, ceiling(StandardProductVendor.InventoryDays * ProductShipmentResponseDays.AverageDailyShipmentQuantity)) as PurchasingQuantity,
	-- 標準単価
	StandardProductVendor.StandardPrice as UnitPrice,
	-- 出荷対応日数
	ProductShipmentResponseDays.ShipmentResponseDays,
	-- 平均リードタイム[日]
	StandardProductVendor.AverageLeadTime,
	-- 在庫数
	ProductShipmentResponseDays.InventoryQuantity,
	-- 未受領数
	ProductShipmentResponseDays.UnclaimedPurchaseQuantity,
	-- １日当たり平均出荷量
	ProductShipmentResponseDays.AverageDailyShipmentQuantity
from
	-- 製品
	Production.Product
	-- 製品別標準購入先ベンダー
	inner join Purchasing.StandardProductVendor
		on	Product.ProductID = StandardProductVendor.ProductID
	-- 製品別の出荷対応日数
	inner join Purchasing.ProductShipmentResponseDays
		on	Product.ProductID = ProductShipmentResponseDays.ProductID
	-- 製品サブカテゴリー
	inner join Production.ProductSubcategory
		on	Product.ProductSubcategoryID = ProductSubcategory.ProductSubcategoryID
	-- 製品カテゴリー
	inner join Production.ProductCategory
		on	ProductSubcategory.ProductCategoryID = ProductCategory.ProductCategoryID
	-- 製品ベンダー
	inner join Purchasing.Vendor
		on	StandardProductVendor.BusinessEntityID = Vendor.BusinessEntityID
where
	Product.MakeFlag = 0
	and ProductShipmentResponseDays.ShipmentResponseDays < StandardProductVendor.AverageLeadTime
go

--select * from Purchasing.ProductRequiringPurchase
--go

--------------------------------------------------------------------------------------
-- データベースを終了し、構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
--ALTER DATABASE AdventureWorks SET OFFLINE
--GO

--EXEC sp_detach_db AdventureWorks
--GO

--USE [master];
--GO

--PRINT 'Finished - ' + CONVERT(varchar, GETDATE(), 121);
--GO


--SET NOEXEC OFF