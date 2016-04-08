CREATE TABLE [CM].[Schools]
(
    [OrgID] NVARCHAR(36) NULL, 
	[SchoolID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [SchoolName] NVARCHAR(128) NULL, 
    [SchoolYear] NVARCHAR(32) NULL, 
    [SchoolRange] NVARCHAR(32) NULL, 
    [SchoolLevel] NVARCHAR(32) NULL , 
    [SchoolNature] NVARCHAR(32) NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT getdate(), 
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NULL DEFAULT getdate(), 
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_Schools] PRIMARY KEY NONCLUSTERED ([SchoolID]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'在读学校ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'SchoolID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'在读学校名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'SchoolName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'在读学校信息',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学校等级（普通学校，区重点，市重点等）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = 'SchoolLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学校性质（不确定，公立，私立）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = 'SchoolNature'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学年制（三年制-八年制）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'SchoolYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学段（小学，中学，高中，完中，九年一贯，十二年一贯）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'SchoolRange'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Schools',
    @level2type = N'COLUMN',
    @level2name = N'OrgID'
GO

CREATE INDEX [IX_Schools_1] ON [CM].[Schools] ([OrgID])
