

CREATE TABLE [PM].[PresentPermissions](
	[CampusID] [nvarchar](36) NOT NULL,
	[PresentID] [nvarchar](36) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL DEFAULT '99990909 00:00:00',
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_PresentPermissions] PRIMARY KEY CLUSTERED 
([CampusID], [PresentID])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]

GO

GO
ALTER TABLE [PM].[PresentPermissions] ADD  CONSTRAINT [DF_PresentPermissions_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [PM].[PresentPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PresentPermissions_Presents] FOREIGN KEY([PresentID])
REFERENCES [PM].[Presents] ([PresentID])
ON DELETE CASCADE
GO
ALTER TABLE [PM].[PresentPermissions] CHECK CONSTRAINT [FK_PresentPermissions_Presents]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name=N'PresentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'PresentPermissions', @level2type=N'COLUMN',@level2name='CampusID'
GO

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
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'生效日期',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissions',
    @level2type = N'COLUMN',
    @level2name = N'StartDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'失效日期，为下一日期的闭合日期',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissions',
    @level2type = N'COLUMN',
    @level2name = N'EndDate'
GO