USE [master]
GO

EXEC sp_attach_db [AdventureWorks], N'/var/opt/mssql/data/AdventureWorks.mdf', N'/var/opt/mssql/data/AdventureWorks_log.ldf'
GO