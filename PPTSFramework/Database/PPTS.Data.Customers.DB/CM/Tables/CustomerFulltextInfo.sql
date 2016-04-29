CREATE TABLE [CM].[CustomerFulltextInfo]
(
	[OwnerID] NVARCHAR(36) NOT NULL,
    [OwnerType] NVARCHAR(32) NULL, 
    [CustomerSearchContent] NVARCHAR(MAX) NULL, 
	[ParentSearchContent] NVARCHAR(MAX) NULL, 
	[CustomerStatus] NVARCHAR(32) NULL,
    [CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NULL DEFAULT GETUTCDATE(),
    [TenantCode] NVARCHAR(36) NULL,
	CONSTRAINT [PK_CustomerFulltextInfo] PRIMARY KEY NONCLUSTERED ([OwnerID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所有者的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'OwnerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所有者的类型(目前类型PotentialCustomers和Customers)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'OwnerType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户搜索内容',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = 'CustomerSearchContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户系统的全文检索信息',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长搜索内容',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'ParentSearchContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户状态(C_Code_Abbr_BO_CTI_CustomerStatus)0=未确认客户信息, 1 = 确认客户信息, 9=无效用户（逻辑删除），10=正式学员',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerFulltextInfo',
    @level2type = N'COLUMN',
    @level2name = N'CustomerStatus'