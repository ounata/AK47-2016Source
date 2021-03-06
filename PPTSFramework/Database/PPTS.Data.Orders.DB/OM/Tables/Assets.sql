
CREATE TABLE [OM].[Assets](
	[AssetID] [nvarchar](36) NOT NULL ,
    [AssetCode] NVARCHAR(64) NULL, 
    [AssetName] NVARCHAR(255) NULL, 
    [AssetType] NVARCHAR(32) NULL, 
    [AssetRefType] NVARCHAR(32) NULL DEFAULT '0', 
    [AssetRefPID] NVARCHAR(64) NULL, 
    [AssetRefID] NVARCHAR(64) NULL, 
    [AccountID] NVARCHAR(36) NULL, 
    [CustomerID] NVARCHAR(36) NULL, 	
    [CustomerCode] NVARCHAR(64) NULL, 
    [CustomerName] NVARCHAR(64) NULL, 
	[ProductID] [nvarchar](36) NOT NULL,
	[ProductCode] [nvarchar](64) NOT NULL,
	[ProductName] [nvarchar](128) NOT NULL,
    [ProductUnit] NVARCHAR(32) NULL, 
    [ProductUnitName] NVARCHAR(64) NULL, 
    [Grade] NVARCHAR(32) NULL, 
    [GradeName] NVARCHAR(64) NULL, 
    [Subject] NVARCHAR(32) NULL, 
    [SubjectName] NVARCHAR(36) NULL, 
    [Catalog] NVARCHAR(32) NULL, 
    [CatalogName] NVARCHAR(64) NULL, 
    [CategoryType] NVARCHAR(32) NULL, 
    [CategoryTypeName] NVARCHAR(64) NULL, 
    [CourseLevel] NVARCHAR(32) NULL, 
    [CourseLevelName] NVARCHAR(64) NULL, 
    [LessonDuration] NVARCHAR(32) NULL, 
    [LessonDurationValue] DECIMAL(18, 2) NULL, 
	[OrderPrice] [decimal](18, 4) NOT NULL DEFAULT 0,
	[OrderAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[PresentAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[TunlandRate] [decimal](18, 4) NOT NULL DEFAULT 1,
	[SpecialRate] [decimal](18, 4) NOT NULL DEFAULT 1,
	[DiscountType] [nvarchar](32) NULL,
	[DiscountRate] [decimal](18, 6) NOT NULL DEFAULT 1,
	[RealPrice] [decimal](18, 4) NOT NULL DEFAULT 0,
	[RealAmount] [decimal](18, 3) NOT NULL DEFAULT 1,
    [ExpirationDate] DATETIME NULL, 
	[AssignedAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[ConfirmedAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[ExchangedAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[DebookedAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
    [ConfirmedMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[ReturnedMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[Amount] decimal(18,4) NOT NULL DEFAULT 0,
    [Price] DECIMAL(18, 4) NOT NULL DEFAULT 0, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL,
	[VersionStartTime] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[VersionEndTime] DATETIME NULL DEFAULT '99990909 00:00:00' ,
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_Assets] PRIMARY KEY NONCLUSTERED 
([AssetID], [VersionStartTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
) ON [PRIMARY]

GO
ALTER TABLE [OM].[Assets] ADD  CONSTRAINT [DF_OrderStocks_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [OM].[Assets] ADD  CONSTRAINT [DF_OrderStocks_ModifyTime]  DEFAULT GETUTCDATE() FOR [ModifyTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'AssetID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计已排数量（课程资产用，排课+，取消-，确认-）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'AssignedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计确认数量（即已上数量，课程资产用，确认+，删除-）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'ConfirmedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计已兑换数量（课程资产用）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'ExchangedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计已退数量（课程资产用，退订+）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'DebookedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购资产表（需要快照）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = NULL,
    @level2name = NULL
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'累计返还金额（课程资产用，买赠退订时使用）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ReturnedMoney'
GO


GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产单价（目前与实际单价相同）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'Price'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'累计已确认金额（课程资产与非课程资产用，确认+，删除-）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ConfirmedMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产类型（0-课程，1-非课程）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'AssetType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'AssetCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产来源（0-订购单）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = 'AssetRefType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产来源ID（订购单明细ID）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = 'AssetRefID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余资产数量（未上的数量，确认-，删除+）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'Amount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'过期日期',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ExpirationDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ProductID'
GO

GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产名称（资产编号+产品名称）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = 'AssetName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'AccountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ProductCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ProductName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'颗粒度代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ProductUnit'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'颗粒度名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ProductUnitName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品年级代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'Grade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品年级名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'GradeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品科目代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'Subject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品科目名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'SubjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品分类代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'Catalog'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品分类名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CatalogName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CategoryType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CategoryTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课程级别代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CourseLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课程级别名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CourseLevelName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次时长代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'LessonDuration'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次时长名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'LessonDurationValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购的原始单价',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'OrderPrice'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购的数量',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'OrderAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'赠送的数量',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'PresentAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'拓路折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'TunlandRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'特殊折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'SpecialRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'折扣类型（参考订购明细）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'DiscountType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'DiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购的实际单价',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'RealPrice'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购的实际数量',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'RealAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产来源PID（订购单ID）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'AssetRefPID'