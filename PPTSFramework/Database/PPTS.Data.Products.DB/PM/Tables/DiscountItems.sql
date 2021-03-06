

CREATE TABLE [PM].[DiscountItems](
	[ItemID] [nvarchar](36) NOT NULL,
	[DiscountID] [nvarchar](36) NOT NULL,
	[DiscountValue] [decimal](18, 2) NOT NULL DEFAULT 0,
	[DiscountStandard] [decimal](18, 2) NOT NULL DEFAULT 0,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_DiscountItems] PRIMARY KEY NONCLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_DiscountItems]    Script Date: 2016/3/24 13:14:12 ******/
CREATE NONCLUSTERED INDEX [IX_DiscountItems] ON [PM].[DiscountItems]
(
	[DiscountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [PM].[DiscountItems] ADD  CONSTRAINT [DF_DiscountItems_ItemID]  DEFAULT newid() FOR [ItemID]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountItems', @level2type=N'COLUMN',@level2name=N'DiscountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣率' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountItems', @level2type=N'COLUMN',@level2name=N'DiscountValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣标准' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountItems', @level2type=N'COLUMN',@level2name=N'DiscountStandard'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'折扣明细表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountItems',
    @level2type = NULL,
    @level2name = NULL