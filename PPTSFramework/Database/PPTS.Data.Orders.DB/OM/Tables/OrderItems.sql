
CREATE TABLE [OM].[OrderItems](
	[OrderID] [nvarchar](36) NOT NULL,
	[SortNo] [int] NOT NULL DEFAULT 1,
	[ItemID] [nvarchar](36) NOT NULL DEFAULT newid(),
    [ItemNo] NVARCHAR(64) NULL, 
	[ProductID] [nvarchar](36) NOT NULL,
	[ProductCode] [nvarchar](64) NOT NULL,
	[ProductName] [nvarchar](128) NOT NULL,
    [ProductUnit] NVARCHAR(32) NULL, 
    [ProductUnitName] NVARCHAR(64) NULL, 
    [ProductCampusID] NVARCHAR(36) NULL, 
    [ProductCampusName] NVARCHAR(64) NULL, 
    [Grade] NVARCHAR(32) NULL, 
    [GradeName] NVARCHAR(64) NULL, 
    [Subject] NVARCHAR(32) NULL, 
    [SubjectName] NVARCHAR(36) NULL, 
    [Catalog] NVARCHAR(32) NULL, 
    [CatalogName] NVARCHAR(64) NULL, 
    [Category] NVARCHAR(32) NULL, 
    [CategoryName] NVARCHAR(64) NULL, 
    [CategoryType] NVARCHAR(32) NULL, 
    [CategoryTypeName] NVARCHAR(64) NULL, 
    [CourseLevel] NVARCHAR(32) NULL, 
    [CourseLevelName] NVARCHAR(64) NULL, 
    [LessonCount] INT NULL, 
    [LessonDuration] NVARCHAR(32) NULL, 
    [LessonDurationValue] DECIMAL(18, 2) NULL, 
    [PeriodDuration] NVARCHAR(32) NULL, 
    [PeriodDurationValue] DECIMAL(18, 2) NULL, 
    [PeriodsOfLesson] DECIMAL(18, 2) NULL, 
	[OrderPrice] [decimal](18, 4) NOT NULL DEFAULT 0,
	[OrderAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[PresentID] [nvarchar](36) NULL,
    [PresentQuato] DECIMAL(18, 2) NULL, 
	[PresentAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[TunlandRate] [decimal](18, 4) NOT NULL DEFAULT 1,
	[SpecialRate] [decimal](18, 4) NOT NULL DEFAULT 1,
	[DiscountType] [nvarchar](32) NULL,
	[DiscountRate] [decimal](18, 6) NOT NULL DEFAULT 1,
	[RealPrice] [decimal](18, 4) NOT NULL DEFAULT 0,
	[RealAmount] [decimal](18, 3) NOT NULL DEFAULT 1,
	[PromotionQuota] [decimal](18, 4) NOT NULL DEFAULT 0,
	[ExpirationDate] [datetime] NULL,
	[JoinedClassID] [nvarchar](36) NULL,
	[RelatedAssetID] [nvarchar](36) NULL,
	[RelatedAssetCode] [nvarchar](64) NULL,
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY NONCLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_OrderItems]    Script Date: 2016/3/24 14:16:52 ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems] ON [OM].[OrderItems]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name='ItemID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原始价格' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'OrderPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原始数量' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'OrderAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'PresentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际赠送数量' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'PresentAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拓路折扣率' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'TunlandRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊折扣率' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'SpecialRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣类型（无折扣，拓路折口，特殊折扣，买赠折扣，其它）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'DiscountType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣率' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'DiscountRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际价格' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'RealPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际数量' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'RealAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠限额' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'PromotionQuota'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'过期日期' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'ExpirationDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'插班班级ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'JoinedClassID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'兑换关联的源资产ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'OrderItems', @level2type=N'COLUMN',@level2name=N'RelatedAssetID'
GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'顺序号',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'SortNo'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'兑换关联的源资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'RelatedAssetCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'ProductCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'ProductName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购明细表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'买赠表配额',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'PresentQuato'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品归属校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'ProductCampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品归属校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'ProductCampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'明细编号=（OrderNo+SortNo)=资产编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'ItemNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品年级代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'Grade'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品年级名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'GradeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品科目代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'Subject'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品科目名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'SubjectName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课程级别代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'CourseLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课程级别名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'CourseLevelName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次时长代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'LessonDuration'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课次时长值',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'LessonDurationValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'颗粒度代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'ProductUnit'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'颗粒度名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'ProductUnitName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型代码（一级）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'CategoryType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'CategoryTypeName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品分类代码（三级）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'Catalog'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品分类名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'CatalogName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品二级类型',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'Category'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品耳机类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'CategoryName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组总课次',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'LessonCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时时长代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'PeriodDuration'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时时长值',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = N'PeriodDurationValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'每课次课时数',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'OrderItems',
    @level2type = N'COLUMN',
    @level2name = 'PeriodsOfLesson'