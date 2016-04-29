CREATE TABLE [CM].[CustomerStaffRelations]
(
	[CustomerID] NVARCHAR(36) NOT NULL , 
    [StaffID] NVARCHAR(36) NOT NULL, 
    [StaffName] NVARCHAR(64) NULL, 
    [StaffJobID] NVARCHAR(36) NOT NULL, 
    [StaffJobName] NVARCHAR(64) NULL,
    [StaffJobOrgID] NVARCHAR(36) NOT NULL, 
    [StaffJobOrgName] NVARCHAR(64) NULL, 
    [RelationType] NVARCHAR(32) NOT NULL,
	[CreatorID] NVARCHAR(36) NULL,
	[CreatorName] NVARCHAR(64) NULL,
	[CreateTime] DATETIME NULL DEFAULT GETUTCDATE(),
    [VersionStartTime] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [VersionEndTime] DATETIME NULL DEFAULT '99990909 00:00:00', 
	[TenantCode] NVARCHAR(36) NULL, 
	[RowUniqueID] NVARCHAR(36) NOT NULL
    CONSTRAINT [PK_CustomerStaffRelations] PRIMARY KEY NONCLUSTERED ([CustomerID], [RelationType], [VersionStartTime]) DEFAULT (CONVERT([nvarchar](36),newid())) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户员工的关系表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_CustomerStaffRelations_1] ON [CM].[CustomerStaffRelations] ([StaffJobID])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1 咨询关系: 学生，咨询师;2 教管关系：学生，学管师;3 教学关系: 学生，老师; 4 电销关系：学生，坐席；5 市场关系：学生，市场专员',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'RelationType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'租户的ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'TenantCode'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位归属组织机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = 'StaffJobOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位归属组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = 'StaffJobOrgName'
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'行ID。自动生成。代码上不使用',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerStaffRelations',
    @level2type = N'COLUMN',
    @level2name = N'RowUniqueID'