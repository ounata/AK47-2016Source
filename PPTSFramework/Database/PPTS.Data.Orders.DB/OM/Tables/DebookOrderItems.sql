
CREATE TABLE [OM].[DebookOrderItems](
	[DebookID] [nvarchar](36) NOT NULL,
	[SortNo] [int] NOT NULL DEFAULT 1,
	[ItemID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[AssetID] [nvarchar](36) NOT NULL,
	[AccountID] [nvarchar](36) NOT NULL,
	[AccountCode] [nvarchar](64) NULL,
	[DebookAmount] [decimal](18, 3) NOT NULL DEFAULT 0,
	[DebookMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
    [PresentAmountOfDebook] DECIMAL(18, 3) NULL DEFAULT 0,
	[ReturnMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[TenantCode] [nvarchar](36) NULL, 
    CONSTRAINT [PK_DebookOrderItems] PRIMARY KEY NONCLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [IX_DebookOrderItems] UNIQUE ([DebookID], [AssetID])
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'DebookOrderItems', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退订ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'DebookOrderItems', @level2type=N'COLUMN',@level2name=N'DebookID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资产ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'DebookOrderItems', @level2type=N'COLUMN',@level2name=N'AssetID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户ID' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'DebookOrderItems', @level2type=N'COLUMN',@level2name=N'AccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退订数量' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'DebookOrderItems', @level2type=N'COLUMN',@level2name=N'DebookAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退订金额' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'DebookOrderItems', @level2type=N'COLUMN',@level2name=N'DebookMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠返还金额' , @level0type=N'SCHEMA',@level0name=N'OM', @level1type=N'TABLE',@level1name=N'DebookOrderItems', @level2type=N'COLUMN',@level2name=N'ReturnMoney'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户编码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'DebookOrderItems',
    @level2type = N'COLUMN',
    @level2name = N'AccountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'顺序号',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'DebookOrderItems',
    @level2type = N'COLUMN',
    @level2name = 'SortNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退订明细表',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'DebookOrderItems',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退订中包含赠送的数量',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'DebookOrderItems',
    @level2type = N'COLUMN',
    @level2name = 'PresentAmountOfDebook'