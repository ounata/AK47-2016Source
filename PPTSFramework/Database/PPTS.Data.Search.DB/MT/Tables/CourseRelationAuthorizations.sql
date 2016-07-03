﻿CREATE TABLE [MT].[CourseRelationAuthorizations]
(
	[ObjectID] NVARCHAR(36) NOT NULL , 
    [ObjectType] NVARCHAR(32) NOT NULL DEFAULT (10), 
	[OrgID] NVARCHAR(36) NULL, 
    [OrgType] NVARCHAR(32) NULL, 
    [OwnerID] NVARCHAR(36) NOT NULL, 
    [OwnerType] NVARCHAR(32) NOT NULL, 
    [CreateTime] DATETIME NULL DEFAULT getutcdate(), 
    [ModifyTime] DATETIME NULL DEFAULT getutcdate(), 
    CONSTRAINT [PK_CourseRelationAuthorizations] PRIMARY KEY ([ObjectType], [ObjectID], [OwnerType], [OwnerID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象ID:OrgID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象类别:"建档关系"(10),预留',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象ID:关联记录授权ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象类型:跟进记录、学情会记录、订购单….',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时所有者关系授权',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = NULL,
    @level2name = NULL
GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象部门ID:机构ID，操作岗位所属部门',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象部门类型:机构类型，操作岗位所属部门类型，即“所属机构”',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseRelationAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'OrgType'
GO

CREATE INDEX [IX_CourseRelationAuthorizations_01] ON [MT].[CourseRelationAuthorizations] ([OwnerID], [OwnerType],[ObjectType])