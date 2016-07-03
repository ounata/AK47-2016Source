CREATE TABLE [CM].[Phones]
(
	[OwnerID] NVARCHAR(36) NOT NULL,
	[ItemID] INT NOT NULL,
    [IsPrimary] INT NOT NULL DEFAULT 1, 
    [PhoneType] NVARCHAR(32) NOT NULL,
	[CountryCode] NVARCHAR(32) NULL DEFAULT '0086',
    [AreaNumber] NVARCHAR(32) NULL, 
    [PhoneNumber] NVARCHAR(255) NULL, 
    [Extension] NVARCHAR(32) NULL,
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NULL DEFAULT GETUTCDATE(),
    [TenantCode] NVARCHAR(36) NULL, 
    [VersionStartTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [VersionEndTime] DATETIME NULL DEFAULT '99990909 00:00:00', 
    CONSTRAINT [PK_Phones] PRIMARY KEY NONCLUSTERED ([OwnerID], [ItemID], [VersionStartTime])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电话类型(C_CODE_ABBR_CUSTOMER_CONTACT_PHONE_TYPE)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'PhoneType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否是主要联系电话',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = 'IsPrimary'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'AreaNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'国家代码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'CountryCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电话号码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'PhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分机号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'Extension'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电话号码表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'电话拥有者的ID（家长ID或学员ID）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'OwnerID'
GO

CREATE INDEX [IX_Phones_OwnerID] ON [CM].[Phones] ([OwnerID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Phones',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO

CREATE INDEX [IX_Phones_PhoneNumber] ON [CM].[Phones] ([PhoneNumber], [AreaNumber])
