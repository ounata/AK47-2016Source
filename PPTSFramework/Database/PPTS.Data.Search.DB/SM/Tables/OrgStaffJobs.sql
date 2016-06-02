CREATE TABLE [SM].[OrgStaffJobs]
(
    [RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NULL, 
	[CampusID] NVARCHAR(36) NOT NULL, 
    [StaffID] NVARCHAR(36) NULL, 
    [StaffName] NVARCHAR(64) NULL,
    [StaffJobID] NVARCHAR(36) NOT NULL, 
    [StaffJobType] NVARCHAR(32) NULL, 
    CONSTRAINT [PK_OrgStaffJobs] PRIMARY KEY ([StaffJobID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工姓名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = N'COLUMN',
    @level2name = N'StaffName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'岗位类型',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'员工岗位表（咨询师，学管师，教师）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'OrgStaffJobs',
    @level2type = NULL,
    @level2name = NULL