
CREATE TABLE [PM].[Categories](
	[CategoryID] [nvarchar](36) NOT NULL CONSTRAINT [DF_Categories_CategoryID]  DEFAULT newid(),
	[Category] [nvarchar](32) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
	[CategoryType] [nvarchar](32) NOT NULL,
	[HasCourse] [int] NOT NULL DEFAULT 0,
    [CanInput] INT NULL DEFAULT 0, 
	[Enabled] [int] NOT NULL CONSTRAINT [DF_Categories_Enabled]  DEFAULT 1,
	[SortNo] [int] NOT NULL CONSTRAINT [DF_Categories_SortNo]  DEFAULT 99,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL CONSTRAINT [DF_Categories_CreateTime]  DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL CONSTRAINT [DF_Categories_ModifyTime]  DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_Categories] PRIMARY KEY NONCLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Categories] UNIQUE NONCLUSTERED 
(
	[Category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] 
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品类型ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'CategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品类型编码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name='Category'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品类型名称' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'CategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品类别枚举（1对1，班组，游学，无课收合作，其它）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'CategoryType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否课程管理' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name='HasCourse'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示顺序' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'SortNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Categories', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类别表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'订购时是否允许录入数量',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Categories',
    @level2type = N'COLUMN',
    @level2name = N'CanInput'