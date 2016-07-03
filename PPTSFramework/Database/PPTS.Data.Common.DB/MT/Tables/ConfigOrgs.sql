CREATE TABLE [MT].[ConfigOrgs]
(
	[OrgID] VARCHAR(36) NOT NULL, 
    [OrgName] NVARCHAR(64) NULL, 
    [OrgType] NVARCHAR(32) NULL, 
    [ParentOrgID] NVARCHAR(36) NULL, 
    [ParentOrgName] NVARCHAR(128) NULL , 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    CONSTRAINT [PK_ConfigOrgs] PRIMARY KEY NONCLUSTERED ([OrgID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'机构类型',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'OrgType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'父机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'ParentOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'父机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = N'COLUMN',
    @level2name = N'ParentOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'配置的机构信息',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'ConfigOrgs',
    @level2type = NULL,
    @level2name = NULL