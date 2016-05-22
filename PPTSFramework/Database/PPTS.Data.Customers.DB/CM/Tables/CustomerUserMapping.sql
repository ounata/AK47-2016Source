CREATE TABLE [CM].[CustomerUserMapping]
(
	[ObjectID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [PassportID] NVARCHAR(36) NULL, 
    [UserID] NVARCHAR(36) NULL, 
    [ObjectType] NVARCHAR(32) NULL DEFAULT '1', 
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [AK_CustomerUserMapping_01] UNIQUE ([PassportID]), 
    CONSTRAINT [AK_CustomerUserMapping_02] UNIQUE ([UserID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户/家长ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUserMapping',
    @level2type = N'COLUMN',
    @level2name = N'ObjectID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户通行证ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUserMapping',
    @level2type = N'COLUMN',
    @level2name = N'PassportID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户用户ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUserMapping',
    @level2type = N'COLUMN',
    @level2name = N'UserID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'类型(1--学员[潜客]，2--家长)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUserMapping',
    @level2type = N'COLUMN',
    @level2name = N'ObjectType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分区类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUserMapping',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO

CREATE INDEX [IX_CustomerUserMapping_01] ON [CM].[CustomerUserMapping] ([PassportID], [ObjectType])

GO

CREATE INDEX [IX_CustomerUserMapping_02] ON [CM].[CustomerUserMapping] ([UserID], [ObjectType])

GO

CREATE INDEX [IX_CustomerUserMapping_03] ON [CM].[CustomerUserMapping] ([ObjectID], [ObjectType])
