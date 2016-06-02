CREATE TABLE [PM].[PresentPermissionsApplies](
	[CampusID] [nvarchar](36) NOT NULL,
	[PresentID] [nvarchar](36) NOT NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_PresentPermissionsApplies] PRIMARY KEY CLUSTERED 
([CampusID], [PresentID])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_PresentPermissionsApplies_Presents] FOREIGN KEY ([PresentID]) REFERENCES [PM].[Presents] ([PresentID]),
) ON [PRIMARY]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = 'CampusID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'买赠ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'PresentID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'买赠授权校区请求表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'PresentPermissionsApplies',
    @level2type = NULL,
    @level2name = NULL