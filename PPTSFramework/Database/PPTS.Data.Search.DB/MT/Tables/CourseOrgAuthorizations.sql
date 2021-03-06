﻿CREATE TABLE [MT].[CourseOrgAuthorizations]
(
	[ObjectID] NVARCHAR(36) NOT NULL , 
    [ObjectType] NVARCHAR(32) NOT NULL, 
	[RelationType] NVARCHAR(32) NOT NULL DEFAULT (10),
    [OwnerID] NVARCHAR(36) NOT NULL, 
    [OwnerType] NVARCHAR(32) NOT NULL, 
    [CreateTime] DATETIME NULL DEFAULT getutcdate(), 
    [ModifyTime] DATETIME NULL DEFAULT getutcdate(), 
    PRIMARY KEY ([ObjectID], [ObjectType], [RelationType], [OwnerID], [OwnerType])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象类别-学科组(XueKeZu-5)、部门(Department-4)、校区(Campus-3)、分公司(Branch-2)、大区(Region-8);',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象ID-关联课表记录ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'OwnerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象类型-课表',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'OwnerType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象类别:"建档关系"(10),预留',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CourseOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'RelationType'
GO

CREATE INDEX [IX_CourseOrgAuthorizations_01] ON [MT].[CourseOrgAuthorizations] ([OwnerID], [OwnerType], [RelationType])

GO

CREATE INDEX [IX_CourseOrgAuthorizations_02] ON [MT].[CourseOrgAuthorizations] ([OwnerID], [OwnerType], [ObjectType], [ObjectID])
