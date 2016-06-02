CREATE TABLE [CM].[CustomerTeacherRelations]
(
	[ID]  NVARCHAR(36) NOT NULL, 
    [CustomerID] NVARCHAR(36) NOT NULL, 
    [TeacherID] NVARCHAR(36) NULL, 
    [TeacherName] NVARCHAR(64) NULL, 
	[TeacherJobID] NVARCHAR(32) NOT NULL, 
    [TeacherOACode] NVARCHAR(128) NULL, 
    [TeacherJobOrgID] NVARCHAR(36) NULL, 
	[TeacherJobOrgName] NVARCHAR(128) NULL, 
    [IsFullTimeTeacher] INT NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [VersionStartTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [VersionEndTime] DATETIME NULL DEFAULT '99990909 00:00:00', 
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerTeacherRelations] PRIMARY KEY NONCLUSTERED ([ID], [VersionStartTime]), 
    CONSTRAINT [IK_CustomerTeacherRelations] UNIQUE ([CustomerID], [TeacherJobID], [VersionStartTime]) 
)

GO

CREATE INDEX [IX_CustomerTeacherRelations_1] ON [CM].[CustomerTeacherRelations] ([TeacherJobID])

GO

CREATE INDEX [IX_CustomerTeacherRelations_2] ON [CM].[CustomerTeacherRelations] ([TeacherID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'TeacherID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'TeacherName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位归属组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位归属组织机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'TeacherJobOrgID'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师学员关系主键',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师OA账号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'TeacherOACode'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员教师分配关系表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否全职教师',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherRelations',
    @level2type = N'COLUMN',
    @level2name = N'IsFullTimeTeacher'