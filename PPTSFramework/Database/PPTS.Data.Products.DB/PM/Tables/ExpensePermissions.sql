

CREATE TABLE [PM].[ExpensePermissions](
	[PermissionID] [nvarchar](36) NOT NULL,
	[ExpenseID] [nvarchar](36) NOT NULL,
	[UseOrgID] [nvarchar](36) NOT NULL,
	[UseOrgType] [nvarchar](32) NOT NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_ExpensePermissions] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ExpensePermissions] UNIQUE NONCLUSTERED 
(
	[UseOrgID] ASC,
	[ExpenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PM].[ExpensePermissions] ADD  CONSTRAINT [DF_ExpensePermissions_PermissionID]  DEFAULT newid() FOR [PermissionID]
GO
ALTER TABLE [PM].[ExpensePermissions] ADD  CONSTRAINT [DF_ExpensePermissions_CreateTime]  DEFAULT getdate() FOR [CreateTime]
GO
ALTER TABLE [PM].[ExpensePermissions]  WITH CHECK ADD  CONSTRAINT [FK_ExpensePermissions_Expenses] FOREIGN KEY([ExpenseID])
REFERENCES [PM].[Expenses] ([ExpenseID])
ON DELETE CASCADE
GO
ALTER TABLE [PM].[ExpensePermissions] CHECK CONSTRAINT [FK_ExpensePermissions_Expenses]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务费ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ExpensePermissions', @level2type=N'COLUMN',@level2name=N'ExpenseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ExpensePermissions', @level2type=N'COLUMN',@level2name=N'UseOrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织类型' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ExpensePermissions', @level2type=N'COLUMN',@level2name=N'UseOrgType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ExpensePermissions', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ExpensePermissions', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ExpensePermissions', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务费用归属权限表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ExpensePermissions',
    @level2type = NULL,
    @level2name = NULL