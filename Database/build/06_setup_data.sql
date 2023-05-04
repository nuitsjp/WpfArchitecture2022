use AdventureWorks;
go

--------------------------------------------------------------------------------------
-- Serilog
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
