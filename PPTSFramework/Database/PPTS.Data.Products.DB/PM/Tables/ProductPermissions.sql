
CREATE TABLE [PM].[ProductPermissions](
	[PermissionID] [nvarchar](36) NOT NULL,
	[ProductID] [nvarchar](36) NOT NULL,
	[UseOrgID] [nvarchar](36) NOT NULL,
	[UseOrgType] [int] NOT NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_ProductPermissions] PRIMARY KEY NONCLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductPermissions] UNIQUE NONCLUSTERED 
(
	[UseOrgID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PM].[ProductPermissions] ADD  CONSTRAINT [DF_ProductPermissions_PermissionID]  DEFAULT newid() FOR [PermissionID]
GO
ALTER TABLE [PM].[ProductPermissions] ADD  CONSTRAINT [DF_ProductPermissions_CreateTime]  DEFAULT getdate() FOR [CreateTime]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductPermissions', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductPermissions', @level2type=N'COLUMN',@level2name=N'UseOrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织类型' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductPermissions', @level2type=N'COLUMN',@level2name=N'UseOrgType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductPermissions', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductPermissions', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductPermissions', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品归属权限表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductPermissions',
    @level2type = NULL,
    @level2name = NULL