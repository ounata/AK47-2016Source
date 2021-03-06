
CREATE TABLE [PM].[CategoryCatalogs](
	[CategoryID] [nvarchar](36) NOT NULL ,
	[CatalogID] [nvarchar](36) NOT NULL CONSTRAINT [DF_CategoryCatalogs_CatalogID]  DEFAULT newid(),
	[Catalog] [nvarchar](32) NOT NULL,
	[CatalogName] [nvarchar](50) NULL,
	[HasPartner] [int] NOT NULL,
	[Eanbled] [int] NOT NULL CONSTRAINT [DF_CategoryCatalogs_Eanbled]  DEFAULT 1,
	[SortNo] [int] NOT NULL CONSTRAINT [DF_CategoryCatalogs_SortNo]  DEFAULT 99,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL CONSTRAINT [DF_CategoryCatalogs_CreateTime]  DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL CONSTRAINT [DF_CategoryCatalogs_ModifyTime]  DEFAULT GETUTCDATE(),
 CONSTRAINT [PK_CategoryCatalogs] PRIMARY KEY NONCLUSTERED 
(
	[CatalogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CategoryCatalogs] UNIQUE NONCLUSTERED 
(
	[Catalog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_CategoryCatalogs_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [PM].[Categories]([CategoryID]) 
) ON [PRIMARY]

GO

GO
ALTER TABLE [PM].[CategoryCatalogs] CHECK CONSTRAINT [FK_CategoryCatalogs_Categories]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品类别ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'CategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品分类ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'CatalogID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品分类编码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name='Catalog'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品分类名称' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'CatalogName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有合作' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'HasPartner'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'Eanbled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示顺序' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'CategoryCatalogs', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品分类表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'CategoryCatalogs',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_CategoryCatalogs_1] ON [PM].[CategoryCatalogs] ([CategoryID])
