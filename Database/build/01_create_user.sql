use AdventureWorks;

--------------------------------------------------------------------------------------
-- AdventureWorks
--------------------------------------------------------------------------------------

-- Login
if exists
    (select name from master.sys.server_principals where name = 'AdventureWorks')
begin
	drop login AdventureWorks
end

create login AdventureWorks with password = 'xR^g*BV2XX8d2p77';
go

-- User
drop user if exists AdventureWorks
go
create user AdventureWorks for login AdventureWorks;
go

-- Schema
drop schema if exists AdventureWorks
go
create schema AdventureWorks
go

---------------------------------------------------------------------------------------
-- Purchasing：購買ドメイン共通
-- AdventureWorksから名前空間のような連続した名前にしても良いが、このDBは購買ドメインでしか
-- 利用しない前提のため、短く保つこととする。
--------------------------------------------------------------------------------------

-- Login
if exists
    (select name from master.sys.server_principals where name = 'Purchasing')
begin
	drop login Purchasing
end

create login Purchasing with password = 'mobPEC4a6N2Dh*';
go

-- User
drop user if exists Purchasing
go
create user Purchasing for login Purchasing;
go

--------------------------------------------------------------------------------------
-- RePurchasing：再発注ユースケース
--------------------------------------------------------------------------------------

-- Login
if exists
    (select name from master.sys.server_principals where name = 'RePurchasing')
begin
	drop login RePurchasing
end

create login RePurchasing with password = '%&^h6cGpWW4Q*u';
go

-- User
drop user if exists RePurchasing
go
create user RePurchasing for login RePurchasing;
go

-- Schema
drop schema if exists RePurchasing
go
create schema RePurchasing
go

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

--------------------------------------------------------------------------------------
-- 構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
CHECKPOINT;
GO
