
CREATE TABLE [PM].[Products](
    [RdOrgID] NVARCHAR(36) NULL, 
    [RdOrgName] NVARCHAR(128) NULL, 
	[ProductID] [nvarchar](36) NOT NULL,
	[ProductCode] [nvarchar](64) NOT NULL,
	[ProductName] [nvarchar](128) NULL,
    [ProductMemo] NVARCHAR(MAX) NULL, 
	[ProductStatus] [nvarchar](32) NULL,
    [ProductVersion] NVARCHAR(32) NULL, 
	[ProductPrice] [decimal](18, 4) NULL DEFAULT 0,
    [ProductCost] [decimal](18, 4) NULL DEFAULT 0, 
	[ProductUnit] [nvarchar](32) NULL,
	[TargetPrice] [decimal](18, 4) NULL DEFAULT 0,
    [TargetPriceMemo] NVARCHAR(255) NULL, 
	[Catalog] [nvarchar](32) NULL,
	[Subject] [nvarchar](32) NULL,
	[Grade] [nvarchar](32) NULL,
	[Season] [nvarchar](32) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[PartnerID] [nvarchar](36) NULL,
	[PartnerRatio] [decimal](18, 2) NULL,
	[PartnerName] [nvarchar](128) NULL,
    [SpecialAllowed] INT NULL DEFAULT 1, 
	[TunlandAllowed] [int] NULL,
	[PresentAllowed] [int] NULL,
	[PromotionAllowed] [int] NULL,
	[PromotionQuota] [decimal](18, 4) NULL,
    [ConfirmStartDate] DATETIME NULL, 
    [ConfirmEndDate] DATETIME NULL, 
    [ConfirmMode] NVARCHAR(32) NULL, 
    [ConfirmStaging] INT NULL, 
	[SubmitterID] [nvarchar](36) NULL,
	[SubmitterName] [nvarchar](64) NULL,
	[SubmitterJobID] [nvarchar](36) NULL,
	[SubmitterJobName] [nvarchar](64) NULL,
	[SubmitTime] [datetime] NULL,
    [ApproverID] NVARCHAR(36) NULL, 
    [ApproverName] NVARCHAR(64) NULL, 
    [ApproverJobID] NVARCHAR(36) NULL, 
    [ApproverJobName] NVARCHAR(64) NULL, 
    [ApproveTime] DATETIME NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL,
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Products] UNIQUE NONCLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_ProductID]  DEFAULT newid() FOR [ProductID]
GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_ShareRatio]  DEFAULT 0 FOR [PartnerRatio]
GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_TunlandAllowed]  DEFAULT 0 FOR [TunlandAllowed]
GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_PresentAllowed]  DEFAULT 0 FOR [PresentAllowed]
GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_PromotionAllowed]  DEFAULT 0 FOR [PromotionAllowed]
GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_PromotionQuota]  DEFAULT 0 FOR [PromotionQuota]
GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [PM].[Products] ADD  CONSTRAINT [DF_Products_ModifyTime]  DEFAULT GETUTCDATE() FOR [ModifyTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品编码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProductCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品名称' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProductName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（审批中，已完成，已拒绝）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProductStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单价' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProductPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'颗粒度代码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ProductUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标价格' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TargetPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类代码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Catalog'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科目代码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年级代码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'季度代码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'Season'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启售日期' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'StartDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'停售日期' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'EndDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'给合作方分成比率' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name='PartnerRatio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合作方ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name='PartnerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合作方名称' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name='PartnerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否允许拓路折扣' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'TunlandAllowed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否允许买赠折扣' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'PresentAllowed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否允许促销优惠' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'PromotionAllowed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'促销优惠限额' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'PromotionQuota'
GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'SubmitterID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'SubmitterName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name='SubmitterJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位名称' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'SubmitterJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'SubmitTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Products', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否允许特殊折扣',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'SpecialAllowed'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成本',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ProductCost'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'收入确认开始日期',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmStartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'收入确认结束日期',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmEndDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'目标单价描述',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'TargetPriceMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品描述',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ProductMemo'
	
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ApproverID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ApproverName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批时间',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ApproveTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品版本代码（0-合同产品，1-新产品）',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = 'ProductVersion'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'研发机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'RdOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'研发机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'RdOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认方式（1-手工确认，2-自动确认）',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmMode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认分期月份，即分几个月确认',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Products',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmStaging'