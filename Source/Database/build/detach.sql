ALTER DATABASE AdventureWorks SET OFFLINE
GO

EXEC sp_detach_db AdventureWorks
GO

USE [master];
GO

PRINT 'Finished - ' + CONVERT(varchar, GETDATE(), 121);
GO


SET NOEXEC OFF
