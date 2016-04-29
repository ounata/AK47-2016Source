CREATE TABLE [dbo].[VersionedOrderItem]
(
	[OrderID] NVARCHAR(36) NOT NULL , 
    [ItemID] INT NOT NULL, 
    [ItemName] NVARCHAR(64) NULL, 
    [VersionStartTime] DATETIME NOT NULL, 
    [VersionEndTime] DATETIME NULL, 
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifyTime] DATETIME NULL, 
    CONSTRAINT [PK_VersionedOrderItem] PRIMARY KEY ([OrderID], [ItemID], [VersionStartTime]) 
)
