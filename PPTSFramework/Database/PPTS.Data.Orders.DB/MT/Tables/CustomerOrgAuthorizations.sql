CREATE TABLE [MT].[CustomerOrgAuthorizations]
(
	[ObjectID] NVARCHAR(36) NOT NULL , 
    [ObjectType] NVARCHAR(32) NOT NULL, 
    [OwnerID] NVARCHAR(36) NOT NULL, 
    [OwnerType] NVARCHAR(32) NOT NULL, 
    [CreateTime] DATETIME NULL DEFAULT getutcdate(), 
    [ModifyTime] DATETIME NULL DEFAULT getutcdate(), 
    PRIMARY KEY ([OwnerType], [OwnerID], [ObjectType], [ObjectID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象ID:OrgID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'授权对象类别:部门(Department-4)、校区(Campus-3)、分公司(Branch-2)、大区(Region-8)',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ObjectType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象ID:学员(潜在客户)ID',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'被授权对象类型:学员、潜在客户',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = 'OwnerType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'MT',
    @level1type = N'TABLE',
    @level1name = N'CustomerOrgAuthorizations',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'