

CREATE TABLE [PM].[PresentPermissions](
	[PermissionID] [nvarchar](36) NOT NULL,
	[PresentID] [nvarchar](36) NOT NULL,
	[UseOrgID] [nvarchar](36) NOT NULL,
	[UseOrgType] [nvarchar](32) NOT NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_PresentPermissions] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PresentPermissions] UNIQUE NONCLUSTERED 
(
	[UseOrgID] ASC,
	[PresentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PM].[PresentPermissions] ADD  CONSTRAINT [DF_PresentPermissions_PermissionID]  DEFAULT newid() FOR [PermissionID]
GO
ALTER TABLE [PM].[PresentPermissions] ADD  CONSTRAINT [DF_PresentPermissions_CreateTime]  DEFAULT getdate() FOR [CreateTime]
GO
ALTER TABLE [PM].[PresentPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PresentPermissions_Presents] FOREIGN KEY([PresentID])
REFERENCES [PM].[Presents] ([PresentID])
ON DELETE CASCADE
GO
ALTER TABLE [PM].[PresentPermissions] CHECK CONSTRAINT [FK_PresentPermissions_Presents]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name=N'PresentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name=N'UseOrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织类型' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name=N'UseOrgType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'买赠归属权限表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissions',
    @level2type = NULL,
    @level2name = NULL