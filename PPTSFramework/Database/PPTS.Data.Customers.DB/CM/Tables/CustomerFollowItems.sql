CREATE TABLE [CM].[CustomerFollowItems]
(
	[FollowID] NVARCHAR(36) NOT NULL, 
    [ItemID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [Subject] NVARCHAR(32) NULL, 
    [Institude] NVARCHAR(128) NULL, 
    [StartDate] DATETIME NULL, 
    [EndDate] DATETIME NULL, 
	[TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerFollowItems] PRIMARY KEY NONCLUSTERED ([ItemID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'跟进ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollowItems',
    @level2type = N'COLUMN',
    @level2name = N'FollowID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'明细ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollowItems',
    @level2type = N'COLUMN',
    @level2name = N'ItemID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'科目代码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollowItems',
    @level2type = N'COLUMN',
    @level2name = N'Subject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'辅导机构',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollowItems',
    @level2type = N'COLUMN',
    @level2name = 'Institude'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'辅导起始时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollowItems',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'辅导终止时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollowItems',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'
GO

CREATE INDEX [IX_CustomerFollowItems_1] ON [CM].[CustomerFollowItems] ([FollowID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'跟进明细表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFollowItems',
    @level2type = NULL,
    @level2name = NULL