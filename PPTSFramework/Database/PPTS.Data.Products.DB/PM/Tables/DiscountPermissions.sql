
CREATE TABLE [PM].[DiscountPermissions](
	[PermissionID] [nvarchar](36) NOT NULL,
	[DiscountID] [nvarchar](36) NOT NULL,
	[UseOrgID] [nvarchar](36) NOT NULL,
	[UseOrgType] [nvarchar](32) NOT NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_DiscountPermissions] PRIMARY KEY NONCLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DiscountPermissions] UNIQUE NONCLUSTERED 
(
	[UseOrgID] ASC,
	[DiscountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PM].[DiscountPermissions] ADD  CONSTRAINT [DF_DiscountPermissions_PermissionID]  DEFAULT newid() FOR [PermissionID]
GO
ALTER TABLE [PM].[DiscountPermissions] ADD  CONSTRAINT [DF_DiscountPermissions_CreateTime]  DEFAULT getdate() FOR [CreateTime]
GO
ALTER TABLE [PM].[DiscountPermissions]  WITH CHECK ADD  CONSTRAINT [FK_DiscountPermissions_Discounts] FOREIGN KEY([DiscountID])
REFERENCES [PM].[Discounts] ([DiscountID])
ON DELETE CASCADE
GO
ALTER TABLE [PM].[DiscountPermissions] CHECK CONSTRAINT [FK_DiscountPermissions_Discounts]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountPermissions', @level2type=N'COLUMN',@level2name=N'DiscountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountPermissions', @level2type=N'COLUMN',@level2name=N'UseOrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用者组织类型' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountPermissions', @level2type=N'COLUMN',@level2name=N'UseOrgType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountPermissions', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountPermissions', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'DiscountPermissions', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'折扣归属权限表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountPermissions',
    @level2type = NULL,
    @level2name = NULL