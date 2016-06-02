CREATE LOGIN [msuser] WITH PASSWORD=N'xueda123$'

GO

CREATE USER [msuser]
	FOR LOGIN [msuser]
	WITH DEFAULT_SCHEMA = [dbo]
GO

EXEC sp_addrolemember 'db_owner', 'msuser'
GO
