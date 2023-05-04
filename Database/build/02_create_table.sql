use AdventureWorks;

--------------------------------------------------------------------------------------
-- Serilog
--------------------------------------------------------------------------------------
drop table if exists Serilog.LogSettings
go

create table Serilog.LogSettings(
	ApplicationName nvarchar(400) not null,
	MinimumLevel nvarchar(max) not null,
	Settings nvarchar(max) not null
	constraint PK_LogSettings primary key (ApplicationName)
)	on [PRIMARY];
go

create table Serilog.Log(
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
	constraint PK_Log primary key clustered (Id asc) 
		with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on, optimize_for_sequential_key = off) on [PRIMARY]
)	on [PRIMARY] textimage_on [PRIMARY]
go

--------------------------------------------------------------------------------------
-- 構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
CHECKPOINT;
GO
