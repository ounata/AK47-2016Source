CREATE TABLE [dbo].[VersionedOrder]
(
	[OrderID] NVARCHAR(36) NOT NULL , 
    [OrderName] NVARCHAR(64) NULL, 
    [Amount] DECIMAL(18, 2) NULL DEFAULT 0, 
    [VersionStartTime] DATETIME NOT NULL, 
    [VersionEndTime] DATETIME NULL, 
    PRIMARY KEY ([OrderID], [VersionStartTime])
)
