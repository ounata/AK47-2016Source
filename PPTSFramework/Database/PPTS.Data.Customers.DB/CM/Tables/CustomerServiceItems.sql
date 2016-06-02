CREATE TABLE [CM].[CustomerServiceItems]
(
	[ServiceID] NVARCHAR(36) NOT NULL, 
    [ItemID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [HandleTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [HandleStatus] NVARCHAR(32) NULL, 
    [HandleMemo] NVARCHAR(255) NULL, 
    [HandlerID] NVARCHAR(36) NOT NULL, 
    [HandlerName] NVARCHAR(64) NULL, 
    [HandlerJobID] NVARCHAR(36) NULL, 
    [HandlerJobName] NVARCHAR(64) NULL, 
	[TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerServiceItems] PRIMARY KEY NONCLUSTERED ([ItemID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'ServiceID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = 'HandleTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客服处理明细',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'HandlerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'HandlerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'HandlerJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'HandlerJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理结果',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'HandleMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'ItemID'
GO

CREATE INDEX [IX_CustomerServiceItems_1] ON [CM].[CustomerServiceItems] ([ServiceID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理状态',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerServiceItems',
    @level2type = N'COLUMN',
    @level2name = N'HandleStatus'