CREATE TABLE [MT].[CustomerRelationAuthorizations]
(
	[ObjectID] NVARCHAR(36) NOT NULL , 
    [ObjectType] NVARCHAR(32) NOT NULL, 
    [OrgID] NVARCHAR(36) NULL, 
    [OrgType] NVARCHAR(32) NULL, 
    [OwnerID] NVARCHAR(36) NOT NULL, 
    [OwnerType] NVARCHAR(32) NOT NULL, 
    [CreateTime] DATETIME NULL DEFAULT getutcdate(), 
    [ModifyTime] DATETIME NULL DEFAULT getutcdate(), 
    CONSTRAINT [PK_CustomerRelationAuthorizations] PRIMARY KEY ([OwnerID], [OwnerType], [ObjectType]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户关系授权',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象ID,JobID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象类别:“咨询关系”、“学管关系”、“教学关系”、“市场关系”、“电销关系”',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象部门ID:机构ID，操作岗位所属部门',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象部门类型:机构类型，操作岗位所属部门类型，即“所属机构”',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'OrgType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象ID:学员(潜在客户)ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象类型:学员、潜在客户',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO

CREATE INDEX [IX_CustomerRelationAuthorizations_01] ON [MT].[CustomerRelationAuthorizations] ([ObjectID], [OrgType], [OwnerID], [OwnerType])
