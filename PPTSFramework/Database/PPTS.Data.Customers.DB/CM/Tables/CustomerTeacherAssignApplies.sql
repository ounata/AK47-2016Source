CREATE TABLE [CM].[CustomerTeacherAssignApplies]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [CustomerTeacherRelationID] NVARCHAR(36) NULL, 
	[ApplyType] NVARCHAR(32) NULL DEFAULT 0,
	[CampusID] NVARCHAR(36) NULL,
	[CampusName]  NVARCHAR(128) NULL,
    [CustomerID] NVARCHAR(36) NOT NULL, 
    [OldTeacherID] NVARCHAR(36) NULL, 
    [OldTeacherJobID] NVARCHAR(36) NULL, 
    [OldTeacherOACode] NVARCHAR(128) NULL, 
    [OldTeacherName] NVARCHAR(64) NULL, 
    [OldTeacherOrgID] NVARCHAR(36) NULL, 
    [OldTeacherOrgShortName] NVARCHAR(64) NULL, 
    [OldTeacherOrgName] NVARCHAR(128) NULL, 
    [NewTeacherID] NVARCHAR(36) NULL, 
    [NewTeacherJobID] NVARCHAR(36) NULL, 
    [NewTeacherOACode] NVARCHAR(128) NULL, 
    [NewTeacherName] NVARCHAR(64) NULL, 
    [NewTeacherOrgID] NVARCHAR(36) NULL, 
    [NewTeacherOrgShortName] NVARCHAR(64) NULL, 
    [NewTeacherOrgName] NVARCHAR(128) NULL, 
    [Reason] NVARCHAR(32) NULL, 
    [ReasonDescription ] NVARCHAR(MAX) NULL, 
    [Status] NVARCHAR(32) NULL DEFAULT (2), 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员分配教师申请',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'请求ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师学员关系主键ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerTeacherRelationID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原有教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'OldTeacherID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原有教师JobID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'OldTeacherJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原有教师OA账号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'OldTeacherOACode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原有教师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'OldTeacherName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原有教师所属机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'OldTeacherOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原有教师所属机构简称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'OldTeacherOrgShortName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'原有教师所属机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'OldTeacherOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'NewTeacherID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新教师JobID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'NewTeacherJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新教师OA账号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'NewTeacherOACode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新教师姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'NewTeacherName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新教师所属机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'NewTeacherOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新教师所属机构简称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'NewTeacherOrgShortName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新教师所属机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'NewTeacherOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'调换、调出原因',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'Reason'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'其他原因描述',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'ReasonDescription '
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态(待提交，已提交，已完成)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO

CREATE INDEX [IX_CustomerTeacherAssignApplies_01] ON [CM].[CustomerTeacherAssignApplies] ([CustomerID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'操作类型(分配-0，调换-1，调出-2)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTeacherAssignApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApplyType'