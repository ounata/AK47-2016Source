
CREATE TABLE [OM].[AssetConsumes](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[CustomerCode] [nvarchar](64) NULL,
	[CustomerName] [nvarchar](128) NULL,
    [AccountID] NVARCHAR(36) NULL, 
    [AssetID] NVARCHAR(36) NULL, 
	[AssetCode] [nvarchar](64) NULL,
    [AssetType] NVARCHAR(32) NULL, 
    [AssetRefType] NVARCHAR(32) NULL, 
    [AssetRefPID] NVARCHAR(36) NULL, 
    [AssetRefID] NVARCHAR(36) NULL, 
    [AssetMoney] DECIMAL(18, 4) NULL, 
	[ConsumeID] [nvarchar](36) NOT NULL DEFAULT newid(),
    [ConsumeType] NVARCHAR(32) NULL DEFAULT '0', 
	[ConsumeMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[ConsumeMemo] [nvarchar](255) NULL,
	[ConsumeTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ConsultantID] [nvarchar](36) NULL,
	[ConsultantName] [nvarchar](64) NULL,
	[ConsultantJobID] [nvarchar](36) NULL,
	[EducatorID] [nvarchar](36) NULL,
	[EducatorName] [nvarchar](64) NULL,
	[EducatorJobID] [nvarchar](36) NULL,
    [Amount] DECIMAL(18, 2) NULL DEFAULT 0, 
    [Price] DECIMAL(18, 4) NULL DEFAULT 0, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AssetConsumes] PRIMARY KEY NONCLUSTERED 
(
	[ConsumeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]

GO
/****** Object:  Index [IX_AssetConsumes_2]    Script Date: 2016/3/23 15:09:01 ******/
CREATE NONCLUSTERED INDEX [IX_AssetConsumes_1] ON [OM].[AssetConsumes]
([CustomerID], [ConsumeTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IX_AssetConsumes_4]    Script Date: 2016/3/23 15:09:01 ******/
CREATE NONCLUSTERED INDEX [IX_AssetConsumes_3] ON [OM].[AssetConsumes]
(
	[ConsumeTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [OM].[AssetConsumes] ADD  CONSTRAINT [DF_AssetConsumes_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入归属校区ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='ConsumeTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消耗金额' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name='ConsumeMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'ConsultantID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'ConsultantName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师岗位ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'ConsultantJobID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'EducatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'EducatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师岗位ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'EducatorJobID'
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠返还消耗记录表' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConsumes'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗说明',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = 'ConsumeMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'收入归属校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗单ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = 'ConsumeID'
GO

GO

GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AssetID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AssetCode'
GO

GO

GO

GO

GO

GO

GO

GO

GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AccountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'消耗类型（0-上课收入，1-非上课收入）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = 'ConsumeType'
GO

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产来源类型',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AssetRefType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产来源PID（存放订购单ID）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AssetRefPID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产来源ID（存放订购明细ID）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AssetRefID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'之前资产剩余价值',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AssetMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产类型（参考资产表）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'AssetType'
GO

GO

GO

GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'返还课次数/课次数',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'Amount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'返还差价/课次单价',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConsumes',
    @level2type = N'COLUMN',
    @level2name = N'Price'
GO
