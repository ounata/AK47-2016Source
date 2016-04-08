
CREATE TABLE [OM].[Assets](
	[AssetID] [nvarchar](36) NOT NULL ,
    [AssetCode] NVARCHAR(64) NULL, 
    [AssetType] NVARCHAR(32) NULL, 
    [AssetSource] NVARCHAR(32) NULL DEFAULT 0, 
    [AssetSourceID] NVARCHAR(64) NULL, 
	[ProductID] [nvarchar](36) NOT NULL,
	[ProductCode] [nvarchar](64) NOT NULL,
	[ProductName] [nvarchar](128) NOT NULL,
    [ProductCampusID] NVARCHAR(36) NULL, 
    [ProductCampusName] NVARCHAR(64) NULL, 
    [ExpirationDate] DATETIME NULL, 
	[AssignedAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[ConfirmedAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[ExchangedAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[UsedOrderAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[UsedPresentAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
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
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_Assets] PRIMARY KEY NONCLUSTERED 
(
	[AssetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [IX_Assets] UNIQUE ([AssetCode])
) ON [PRIMARY]

GO
ALTER TABLE [OM].[Assets] ADD  CONSTRAINT [DF_OrderStocks_CreateTime]  DEFAULT getdate() FOR [CreateTime]
GO
ALTER TABLE [OM].[Assets] ADD  CONSTRAINT [DF_OrderStocks_ModifyTime]  DEFAULT getdate() FOR [ModifyTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'AssetID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已排数量（课程资产用）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'AssignedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已上数量（课程资产用）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'ConfirmedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已兑换数量（课程资产用）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'ExchangedAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已退数量（课程资产用）' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'Assets', @level2type=N'COLUMN',@level2name=N'DebookedAmount'
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
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已使用订购数量（课程资产用）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'UsedOrderAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已使用赠送数量（课程资产用）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'UsedPresentAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'返还金额（课程资产用）',
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
    @value = N'当前单价（针对课时资产由于退订可能与订购时不同）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'Price'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'已确认金额（课程资产与非课程资产用）',
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
    @value = N'资产来源（0-订单）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = 'AssetSource'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'资产来源ID（订单明细ID）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = 'AssetSourceID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前数量',
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
    @value = N'产品归属校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ProductCampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品归属校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'Assets',
    @level2type = N'COLUMN',
    @level2name = N'ProductCampusName'
GO

CREATE INDEX [IX_Assets_1] ON [OM].[Assets] ([AssetSourceID], [AssetSource])
