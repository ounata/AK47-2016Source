

CREATE TABLE [PM].[PresentItems](
	[ItemID] [nvarchar](36) NOT NULL,
	[PresentID] [nvarchar](36) NOT NULL,
	[PresentValue] [decimal](18, 2) NOT NULL DEFAULT 0,
	[PresentStandard] [decimal](18, 2) NOT NULL DEFAULT 0,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_PresentItems] PRIMARY KEY NONCLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_PresentItems]    Script Date: 2016/3/24 13:18:54 ******/
CREATE NONCLUSTERED INDEX [IX_PresentItems] ON [PM].[PresentItems]
(
	[PresentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [PM].[PresentItems] ADD  CONSTRAINT [DF_PresentItems_ItemID]  DEFAULT newid() FOR [ItemID]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentItems', @level2type=N'COLUMN',@level2name=N'PresentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠率' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentItems', @level2type=N'COLUMN',@level2name=N'PresentValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠标准' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentItems', @level2type=N'COLUMN',@level2name=N'PresentStandard'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'买赠明细表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentItems',
    @level2type = NULL,
    @level2name = NULL