---------------------------------------------------------------------------------------
-- Purchasing：購買ドメイン共通
-- AdventureWorksから名前空間のような連続した名前にしても良いが、このDBは購買ドメインでしか
-- 利用しない前提のため、短く保つこととする。
--------------------------------------------------------------------------------------
use AdventureWorks;

---------------------------------------------------------------------------------------
-- Login
--------------------------------------------------------------------------------------
if exists
    (select name from master.sys.server_principals where name = 'Purchasing')
begin
	drop login Purchasing
end

create login Purchasing with password = 'mobPEC4a6N2Dh*';
go

---------------------------------------------------------------------------------------
-- User
--------------------------------------------------------------------------------------
drop user if exists Purchasing
go
create user Purchasing for login Purchasing;
go

--------------------------------------------------------------------------------------
-- View : vVendor
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vVendor
go

create view Purchasing.vVendor 
as
select
	Vendor.BusinessEntityID as VendorId,
	Vendor.AccountNumber,
	Vendor.Name,
	Vendor.CreditRating,
	Vendor.PreferredVendorStatus as IsPreferredVendor,
	Vendor.ActiveFlag as IsActive,
	Vendor.PurchasingWebServiceURL as PurchasingWebServiceUrl,
	StateProvince.StateProvinceID,
	-- 税率が設定されていないデータがあるが、おそらくデータ不備のため一律10%を適用する
	case
		when SalesTaxRate.TaxRate is null then 10.0
		else SalesTaxRate.TaxRate
	end as TaxRate,
	Vendor.ModifiedDate as ModifiedDateTime
from
	Purchasing.Vendor
	inner join Person.BusinessEntityAddress
		on	Vendor.BusinessEntityID = BusinessEntityAddress.BusinessEntityID
	inner join Person.Address
		on	BusinessEntityAddress.AddressID = Address.AddressID
	inner join Person.StateProvince
		on	Address.StateProvinceID = StateProvince.StateProvinceID
	left outer join Sales.SalesTaxRate
		on	StateProvince.StateProvinceID = SalesTaxRate.StateProvinceID
go

--------------------------------------------------------------------------------------
-- View : vProductVendor
-- ベンダーの取扱製品
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vProductVendor
go

create view Purchasing.vProductVendor 
as
select
	BusinessEntityID as VendorId,
	ProductID as ProductId,
	AverageLeadTime,
	StandardPrice,
	LastReceiptCost,
	MinOrderQty as MinOrderQuantity,
	MaxOrderQty as MaxOrderQuantity,
	OnOrderQty as OnOrderQuantity,
	UnitMeasureCode,
	ModifiedDate as ModifiedDateTime
from
	Purchasing.ProductVendor
GO

--------------------------------------------------------------------------------------
-- View : vShipMethod
-- 支払方法
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vShipMethod
go

create view Purchasing.vShipMethod 
as
select
	ShipMethodID as ShipMethodId,
	Name,
	ShipBase,
	ShipRate,
	ModifiedDate as ModifiedDateTime
from
	Purchasing.ShipMethod
go

--------------------------------------------------------------------------------------
-- View : vProduct
-- 製品
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vProduct
go

create view Purchasing.vProduct 
as
select
	ProductId,
	Name,
	ProductNumber,
	ISNULL(Color, '') as Color,
	StandardCost as StandardPrice,
	ListPrice,
	ISNULL(Weight, 0) as Weight,
	ModifiedDate as ModifiedDateTime
from
	Production.Product
go


--------------------------------------------------------------------------------------
-- View : vPurchaseOrder
-- 発注
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vPurchaseOrder
go

create view Purchasing.vPurchaseOrder
as
select
	EmployeeID,
	VendorID,
	ShipMethodID,
	ShipDate,
	SubTotal,
	TaxAmt,
	Freight
from
	Purchasing.PurchaseOrderHeader
go

--------------------------------------------------------------------------------------
-- View : vPurchaseOrderDetail
-- 発注明細
--------------------------------------------------------------------------------------
drop view if exists Purchasing.vPurchaseOrderDetail
go

create view Purchasing.vPurchaseOrderDetail
as
select
	PurchaseOrderID,
	DueDate,
	OrderQty,
	ProductID,
	UnitPrice,
	ReceivedQty,
	RejectedQty
from
	Purchasing.PurchaseOrderDetail
go

---------------------------------------------------------------------------------------
-- Grant
--------------------------------------------------------------------------------------
grant select on Purchasing.vProductVendor to Purchasing;
grant select on Purchasing.vVendor to Purchasing;
grant select on Purchasing.vProduct to Purchasing;
grant select on Purchasing.vShipMethod to Purchasing;
grant select on Purchasing.vProduct to Purchasing;
grant insert on Purchasing.vPurchaseOrder to Purchasing;
grant insert on Purchasing.vPurchaseOrderDetail to Purchasing;

--------------------------------------------------------------------------------------
-- 構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
CHECKPOINT;
GO
