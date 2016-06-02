
CREATE TABLE [OM].[AssetConfirms](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[CustomerCode] [nvarchar](64) NULL,
	[CustomerName] [nvarchar](128) NULL,
    [AccountID] NVARCHAR(36) NULL, 
    [AssetID] NVARCHAR(36) NULL, 
	[AssetCode] [nvarchar](64) NULL,
	[ConfirmID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[ConfirmType] [nvarchar](32) NOT NULL,
    [ConfirmFlag] INT NULL DEFAULT 1, 
	[ConfirmMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[ConfirmMemo] [nvarchar](255) NULL,
	[ConfirmStatus] [nvarchar](32) NOT NULL,
	[ConfirmTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ConfirmerID] [nvarchar](36) NULL,
	[ConfirmerName] [nvarchar](64) NULL,
	[ConfirmerJobID] [nvarchar](36) NULL,
	[ConfirmerJobName] [nvarchar](64) NULL,
	[ConfirmerJobType] [nvarchar](32) NULL,
    [ProcessStatus] NVARCHAR(32) NOT NULL DEFAULT 0, 
    [ProcessTime] DATETIME NULL, 
    [ProcessMemo] NVARCHAR(255) NULL, 
	[ConsultantID] [nvarchar](36) NULL,
	[ConsultantName] [nvarchar](64) NULL,
	[ConsultantJobID] [nvarchar](36) NULL,
	[EducatorID] [nvarchar](36) NULL,
	[EducatorName] [nvarchar](64) NULL,
	[EducatorJobID] [nvarchar](36) NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NULL, 
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AssetConfirms] PRIMARY KEY NONCLUSTERED 
(
	[ConfirmID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AssetConfirms_1]    Script Date: 2016/3/23 15:09:01 ******/


GO
/****** Object:  Index [IX_AssetConfirms_2]    Script Date: 2016/3/23 15:09:01 ******/
CREATE NONCLUSTERED INDEX [IX_AssetConfirms_1] ON [OM].[AssetConfirms]
([CustomerID], [ConfirmTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [IX_AssetConfirms_4]    Script Date: 2016/3/23 15:09:01 ******/
CREATE NONCLUSTERED INDEX [IX_AssetConfirms_3] ON [OM].[AssetConfirms]
(
	[ConfirmTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [OM].[AssetConfirms] ADD  CONSTRAINT [DF_AssetConfirms_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认类型（0-非课程，1-课程类）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认状态（1-已确认，3-已删除 ）参考排课' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人岗位ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmerJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人岗位名称' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmerJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认金额' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name='ConfirmMoney'
GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'ConsultantID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'ConsultantName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师岗位ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'ConsultantJobID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'EducatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'EducatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师岗位ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'EducatorJobID'
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

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产收入确认记录表' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'AssetConfirms'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认说明',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = 'ConfirmMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认单ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = 'ConfirmID'
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
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'AssetID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'AssetCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理状态（参考订购）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ProcessStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ProcessTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理说明',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ProcessMemo'
GO

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认人岗位类型代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmerJobType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'AccountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认标志（1-收入确认，-1收入取消）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'AssetConfirms',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmFlag'