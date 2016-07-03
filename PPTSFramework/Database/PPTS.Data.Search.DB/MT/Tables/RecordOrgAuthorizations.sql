CREATE TABLE [MT].[RecordOrgAuthorizations]
(
	[ObjectID] NVARCHAR(36) NOT NULL , 
    [ObjectType] NVARCHAR(32) NOT NULL, 
	[RelationType] NVARCHAR(32) NOT NULL DEFAULT (10),
    [OwnerID] NVARCHAR(36) NOT NULL, 
    [OwnerType] NVARCHAR(32) NOT NULL, 
    [CreateTime] DATETIME NULL DEFAULT getutcdate(), 
    [ModifyTime] DATETIME NULL DEFAULT getutcdate(), 
    CONSTRAINT [PK_RecordOrgAuthorizations] PRIMARY KEY ([OwnerType], [ObjectID], [ObjectType], [RelationType], [OwnerID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象ID:OrgID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象类别:部门(Department-4)、校区(Campus-3)、分公司(Branch-2)、大区(Region-8)',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象ID:关联记录授权ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象类型:跟进记录、学情会记录、订购单….',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'记录机构授权',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = NULL,
    @level2name = NULL
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象类别:"建档关系"(10),预留',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'RecordOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'RelationType'
GO

CREATE INDEX [IX_RecordOrgAuthorizations_01] ON [MT].[RecordOrgAuthorizations] ([OwnerID], [OwnerType], [RelationType])

GO

CREATE INDEX [IX_RecordOrgAuthorizations_02] ON [MT].[RecordOrgAuthorizations] ([OwnerID], [OwnerType], [ObjectType], [ObjectID])
