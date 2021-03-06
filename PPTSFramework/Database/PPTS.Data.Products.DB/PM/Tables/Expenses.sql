 
CREATE TABLE [PM].[Expenses](
	[ExpenseID] [nvarchar](36) NOT NULL,
	[ExpenseType] [nvarchar](32) NOT NULL,
	[ExpenseValue] [decimal](18, 4) NOT NULL DEFAULT 0,
    [BranchID] NVARCHAR(36) NULL, 
	[BranchName] [nvarchar](64) NULL,
    [CampusIDs] NVARCHAR(MAX) NULL, 
	[CampusNames] [nvarchar](MAX) NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL,
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY NONCLUSTERED 
(
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]

GO
/****** Object:  Index [IX_Expenses_1]    Script Date: 2016/3/24 13:17:41 ******/
ALTER TABLE [PM].[Expenses] ADD  CONSTRAINT [DF_Expenses_ExpenseID]  DEFAULT newid() FOR [ExpenseID]
GO
ALTER TABLE [PM].[Expenses] ADD  CONSTRAINT [DF_Expenses_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [PM].[Expenses] ADD  CONSTRAINT [DF_Expenses_ModifyTime]  DEFAULT GETUTCDATE() FOR [ModifyTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务费ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'ExpenseID'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务费类型（1- 一对一，2-班组, 3-综合(所有)）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'ExpenseType'
GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区名称列表，逗号分开，仅仅展示用' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name='CampusNames'
GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Expenses', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务费用表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Expenses',
    @level2type = NULL,
    @level2name = NULL
	
GO


GO

GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务费金额',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Expenses',
    @level2type = N'COLUMN',
    @level2name = N'ExpenseValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Expenses',
    @level2type = N'COLUMN',
    @level2name = 'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司名称',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Expenses',
    @level2type = N'COLUMN',
    @level2name = 'BranchName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID列表，逗号分开',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Expenses',
    @level2type = N'COLUMN',
    @level2name = N'CampusIDs'