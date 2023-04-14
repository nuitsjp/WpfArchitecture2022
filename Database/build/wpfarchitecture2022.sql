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
drop view if exists Purchasing.vStandardProductVendor
go

create view Purchasing.vStandardProductVendor 
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

--select * from Purchasing.vStandardProductVendor


--------------------------------------------------------------------------------------
-- すべての製品の製品別在庫
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vProductInventory
go

create view Purchasing.vProductInventory as
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

--select * from Purchasing.vProductInventory
go

--------------------------------------------------------------------------------------
-- すべての製品の製品別未受領数
-- 発注しているがまだ未受領の数量
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vProductUnclaimedPurchase
go

create view Purchasing.vProductUnclaimedPurchase as
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

--select * from Purchasing.vProductUnclaimedPurchase
go

--------------------------------------------------------------------------------------
-- 販売実績のある製品の、製品別の１日当たり平均出荷量（不定期不定量発注方式の用語）
-- 実際には販売量を利用する
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vProductAverageDailyShipment
go

create view Purchasing.vProductAverageDailyShipment as
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

--select * from Purchasing.vProductAverageDailyShipment
go

--------------------------------------------------------------------------------------
-- 販売実績のある製品の、製品別の出荷対応日数（不定期不定量発注方式の用語）
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vProductShipmentResponseDays
go

create view Purchasing.vProductShipmentResponseDays
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
	Purchasing.vProductAverageDailyShipment
	inner join Purchasing.vProductInventory
		on	vProductAverageDailyShipment.ProductID = vProductInventory.ProductID
	inner join Purchasing.vProductUnclaimedPurchase
		on	vProductAverageDailyShipment.ProductID = vProductUnclaimedPurchase.ProductID
go

--select * from Purchasing.vProductShipmentResponseDays order by ShipmentResponseDays
go

--------------------------------------------------------------------------------------
-- 要購入製品
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vProductRequiringPurchase
go

create view Purchasing.vProductRequiringPurchase as
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
	inner join Purchasing.vStandardProductVendor
		on	Product.ProductID = vStandardProductVendor.ProductID
	-- 製品別の出荷対応日数
	inner join Purchasing.vProductShipmentResponseDays
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

--select * from Purchasing.vProductRequiringPurchase
--go

--------------------------------------------------------------------------------------
-- Serilog
-- ログの出力先と、出力設定に関するオブジェクト
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
create table Serilog.LogSettings(
    ApplicationName nvarchar(400) not null,
	MinimumLevel nvarchar(max) not null,
	Settings nvarchar(max) not null
	constraint PK_LogSettings primary key (ApplicationName)
)	on [PRIMARY];
go



create table Serilog.Logs(
	Id int identity(1,1) not null,
	TimeStamp datetime null,
	Level nvarchar(max) null,
	Message nvarchar(max) null,
	Exception nvarchar(max) null,
	ApplicationType nvarchar(max) null,
	Application nvarchar(max) null,
	MachineName nvarchar(max) null,
	Peer nvarchar(max) null,
	EmployeeId int null,
	ProcessId int null,
	ThreadId int null,
	LogEvent nvarchar(max) null,
	constraint PK_Logs primary key clustered (Id asc) 
		with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, optimize_for_sequential_key = off) on [PRIMARY]
)	on [PRIMARY] textimage_on [PRIMARY]
go

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

-- Grant
grant select on Serilog.vLogSettings to Serilog;
grant insert, select on Serilog.Logs to Serilog;

-- デフォルト設定の登録
insert into 
	Serilog.LogSettings
values (
	'Server Default',
	'Information',
	'{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Debug", "Serilog.Sinks.MSSqlServer" ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId" ],
    "Properties": {
      "Application": "%ApplicationName%",
      "ApplicationType": "ASP.NET Core"
    },
	"MinimumLevel": {
      "Default": "%MinimumLevel%",
      "Override": {
        "Microsoft": "Warning",
        "Grpc": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "MSSqlServer",
        "Args": {
          "restrictedToMinimumLevel": "%MinimumLevel%",
          "connectionString": "%ConnectionString%",
          "sinkOptions": {
            "SchemaName": "Serilog",
            "TableName": "Logs",
            "AutoCreateSqlTable": true,
            "batchPostingLimit": 1000,
            "period": "0.00:00:30"
          },
		  "columnOptionsSection": {
            "disableTriggers": true,
            "clusteredColumnstoreIndex": false,
            "primaryKeyColumnName": "Id",
			"addStandardColumns": [ "LogEvent" ],
			"removeStandardColumns": [ "MessageTemplate", "Properties" ],            
			"additionalColumns": [
              {
                "ColumnName": "ApplicationType",
                "PropertyName": "ApplicationType",
                "DataType": "nvarchar"
              },
              {
                "ColumnName": "Application",
                "PropertyName": "Application",
                "DataType": "nvarchar"
              },
              {
                "ColumnName": "MachineName",
                "PropertyName": "MachineName",
                "DataType": "nvarchar"
              },
              {
                "ColumnName": "Peer",
                "PropertyName": "Peer",
                "DataType": "nvarchar"
              },
              {
                "ColumnName": "EmployeeId",
                "PropertyName": "EmployeeId",
                "DataType": "int"
              },
              {
                "ColumnName": "ProcessId",
                "PropertyName": "ProcessId",
                "DataType": "int"
              },
              {
                "ColumnName": "ThreadId",
                "PropertyName": "ThreadId",
                "DataType": "int"
              }
            ]
          },
		  "logEvent": {
			"excludeAdditionalProperties": true,
			"excludeStandardColumns": true
		  },
        }
      }
    ],
    "Destructure": [
      {
        "Name": "ToMaximumDepth",
        "Args": { "maximumDestructuringDepth": 4 }
      },
      {
        "Name": "ToMaximumStringLength",
        "Args": { "maximumStringLength": 100 }
      },
      {
        "Name": "ToMaximumCollectionCount",
        "Args": { "maximumCollectionCount": 10 }
      }
    ]
  }
}'
)

 insert into 
	Serilog.LogSettings
values (
	'Client Default',
	'Information',
	'{
  "Serilog": {
    "Using": [ "Serilog.Sinks.File" ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithEnvironmentUserName", "WithProcessId", "WithThreadId" ],
    "MinimumLevel": {
      "Default": "%MinimumLevel%",
      "Override": {
        "Microsoft": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Logs/Log.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7,
		  "restrictedToMinimumLevel": "Information",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [PID:{ProcessId}] [TID:{ThreadId}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}'
)


--------------------------------------------------------------------------------------
-- データベースを終了し、構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
ALTER DATABASE AdventureWorks SET OFFLINE
GO

EXEC sp_detach_db AdventureWorks
GO

USE [master];
GO

PRINT 'Finished - ' + CONVERT(varchar, GETDATE(), 121);
GO


SET NOEXEC OFF