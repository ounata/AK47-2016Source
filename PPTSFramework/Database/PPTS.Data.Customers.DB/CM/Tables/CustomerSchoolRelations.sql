CREATE TABLE [CM].[CustomerSchoolRelations]
(
    [CustomerID] NVARCHAR(36) NOT NULL, 
    [SchoolID] NVARCHAR(36) NOT NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT getdate(), 
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NULL DEFAULT getdate(), 
    CONSTRAINT [PK_CustomerSchoolRelations] PRIMARY KEY NONCLUSTERED ([SchoolID], [CustomerID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户在读学校关系表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'在读学校ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'SchoolID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerSchoolRelations',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'