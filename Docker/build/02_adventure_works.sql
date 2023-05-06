use AdventureWorks;

--------------------------------------------------------------------------------------
-- Login
--------------------------------------------------------------------------------------
if exists
    (select name from master.sys.server_principals where name = 'AdventureWorks')
begin
	drop login AdventureWorks
end

create login AdventureWorks with password = 'xR^g*BV2XX8d2p77';
go

--------------------------------------------------------------------------------------
-- User
--------------------------------------------------------------------------------------
drop user if exists AdventureWorks
go
create user AdventureWorks for login AdventureWorks;
go

--------------------------------------------------------------------------------------
-- Schema
--------------------------------------------------------------------------------------
drop schema if exists AdventureWorks
go
create schema AdventureWorks
go

--------------------------------------------------------------------------------------
-- Function : GetToday
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

--------------------------------------------------------------------------------------
-- View : vUser
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
-- Grant
--------------------------------------------------------------------------------------
grant select on AdventureWorks.vUser to AdventureWorks;

--------------------------------------------------------------------------------------
-- 構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
CHECKPOINT;
GO
