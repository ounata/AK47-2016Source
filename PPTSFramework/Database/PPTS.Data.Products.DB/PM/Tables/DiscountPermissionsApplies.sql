CREATE TABLE [PM].[DiscountPermissionsApplies](
	[CampusID] [nvarchar](36) NOT NULL,
	[DiscountID] [nvarchar](36) NOT NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_DiscountPermissionsApplies] PRIMARY KEY NONCLUSTERED 
([CampusID], [DiscountID])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [FK_DiscountPermissionsApplies_Discounts] FOREIGN KEY ([DiscountID]) REFERENCES [PM].[Discounts]([DiscountID]) 
) ON [PRIMARY]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = 'CampusID'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'折扣ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'DiscountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountPermissionsApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'拓路折扣授权请求表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'DiscountPermissionsApplies',
    @level2type = NULL,
    @level2name = NULL