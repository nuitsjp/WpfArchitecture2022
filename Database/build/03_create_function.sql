use AdventureWorks;

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

--------------------------------------------------------------------------------------
-- 平均リードタイムから、不定期不定量発注方式の在庫日数を取得する
--------------------------------------------------------------------------------------
drop function if exists RePurchasing.GetInventoryDays
go

create function RePurchasing.GetInventoryDays(
	@AverageLeadTime int
)
returns int
as
begin
	return @AverageLeadTime + GREATEST(4, CEILING(@AverageLeadTime * 0.2));
end
go

--------------------------------------------------------------------------------------
-- 日別平均出荷数を求める期間の日数を取得する
--------------------------------------------------------------------------------------
drop function if exists RePurchasing.GetAverageDailyShipmentsPeriodDays
go

create function RePurchasing.GetAverageDailyShipmentsPeriodDays()
returns int
as
begin
	return 90;
end
go

--------------------------------------------------------------------------------------
-- 構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
CHECKPOINT;
GO
