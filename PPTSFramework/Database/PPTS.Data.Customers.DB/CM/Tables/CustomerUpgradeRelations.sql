CREATE TABLE [CM].[CustomerUpgradeRelations]
(
	[UpgradeID] NVARCHAR(36) NOT NULL PRIMARY KEY DEFAULT (newid()), 
    [CustomerGrade] NVARCHAR(32) NOT NULL, 
    [UpgradedGrade] NVARCHAR(32) NOT NULL, 
    [UpgradeType] NVARCHAR(32) NOT NULL, 
    [CreateTime] DATETIME NOT NULL DEFAULT GETUTCDATE()
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUpgradeRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'升级类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUpgradeRelations',
    @level2type = N'COLUMN',
    @level2name = N'UpgradeType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'升级ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUpgradeRelations',
    @level2type = N'COLUMN',
    @level2name = N'UpgradeID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'升级前年级',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUpgradeRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerGrade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'升级后年级',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerUpgradeRelations',
    @level2type = N'COLUMN',
    @level2name = N'UpgradedGrade'