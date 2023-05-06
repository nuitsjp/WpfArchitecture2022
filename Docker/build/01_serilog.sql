--------------------------------------------------------------------------------------
-- Serilog
-- ログの出力先と、出力設定に関するオブジェクト
--------------------------------------------------------------------------------------
use AdventureWorks;

--------------------------------------------------------------------------------------
-- Login
--------------------------------------------------------------------------------------
if exists
    (select name from master.sys.server_principals where name = 'Serilog')
begin
	drop login Serilog
end

create login Serilog with password = 'RG3CbVP!2U4hT5';
go

--------------------------------------------------------------------------------------
-- User
--------------------------------------------------------------------------------------
drop user if exists Serilog
go
create user Serilog for login Serilog;
go

--------------------------------------------------------------------------------------
-- Schema
--------------------------------------------------------------------------------------
drop schema if exists Serilog
go
create schema Serilog
go

--------------------------------------------------------------------------------------
-- Table : LogSettings
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

--------------------------------------------------------------------------------------
-- Table : Log
--------------------------------------------------------------------------------------
drop table if exists Serilog.Log
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
-- View : vLogSettings
-- ログの出力先と、出力設定に関するオブジェクト
--------------------------------------------------------------------------------------
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
-- View : vLog
-- ログ
--------------------------------------------------------------------------------------
drop view if exists Serilog.vLog
go

create view Serilog.vLog
as
select
    Message,
    Level,
    TimeStamp,
    Exception,
    ApplicationType,
    Application,
    MachineName,
    Peer,
    EmployeeId,
    ProcessId,
    ThreadId,
    LogEvent
from
	Serilog.Log
go

--------------------------------------------------------------------------------------
-- Grant
--------------------------------------------------------------------------------------
grant select on Serilog.vLogSettings to Serilog;
grant insert on Serilog.vLog to Serilog;

--------------------------------------------------------------------------------------
-- Insert : LogSettings
--------------------------------------------------------------------------------------
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
          "formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact",
          "sinkOptions": {
            "SchemaName": "Serilog",
            "TableName": "Log",
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
    "Using": [ "Serilog.Sinks.File", "AdventureWorks.Logging.Serilog.MagicOnion" ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithEnvironmentUserName", "WithProcessId", "WithThreadId" ],
    "Properties": {
      "Application": "%ApplicationName%",
      "ApplicationType": "WPF"
    },
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
          "path": "Log/Log.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7,
		  "restrictedToMinimumLevel": "Information",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [PID:{ProcessId}] [TID:{ThreadId}] {Message:lj}{NewLine}{Exception}"
        }
      },
	  {
        "Name": "MagicOnion",
        "Args": {
		  "restrictedToMinimumLevel": "Debug"
		}
      }
    ]
  }
}'
)

--------------------------------------------------------------------------------------
-- 構築した内容をファイルへ書き出す
--------------------------------------------------------------------------------------
CHECKPOINT;
GO
