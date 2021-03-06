
CREATE TABLE [OM].[ShoppingCarts](
	[CartID] [nvarchar](36) NOT NULL DEFAULT newid(),
    [OrderType] NVARCHAR(32) NULL, 
	[CustomerID] [nvarchar](36) NOT NULL,
	[ProductID] [nvarchar](36) NOT NULL,
    [ProductCampusID] NVARCHAR(36) NULL, 
    [ClassID] NVARCHAR(36) NULL, 
	[Amount] [decimal](18, 2) NOT NULL DEFAULT 1,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY NONCLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ShoppingCarts] UNIQUE NONCLUSTERED 
([CustomerID], [ProductID], [OrderType])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购物车ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ShoppingCarts', @level2type=N'COLUMN',@level2name=N'CartID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ShoppingCarts', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ShoppingCarts', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订购数量' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ShoppingCarts', @level2type=N'COLUMN',@level2name=N'Amount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ShoppingCarts', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ShoppingCarts', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'ShoppingCarts', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO


GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'购物车表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ShoppingCarts',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品归属校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ShoppingCarts',
    @level2type = N'COLUMN',
    @level2name = 'ProductCampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班级ID（针对插班订购）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ShoppingCarts',
    @level2type = N'COLUMN',
    @level2name = N'ClassID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购类型（参考订购表）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'ShoppingCarts',
    @level2type = N'COLUMN',
    @level2name = N'OrderType'