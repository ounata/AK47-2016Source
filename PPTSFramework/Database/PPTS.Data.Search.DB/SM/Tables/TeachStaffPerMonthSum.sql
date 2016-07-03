﻿CREATE TABLE [SM].[TeachStaffPerMonthSum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NULL, 
    [CampusID] NVARCHAR(36) NULL, 
    [GroupID] NVARCHAR(36) NULL, 
    [StaffID] NVARCHAR(36) NULL, 
    [StaffJobID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [HourCount] DECIMAL(18, 4) NULL, 
    [HourCountTask] DECIMAL(18, 4) NULL, 
    [RenewalMoney] DECIMAL(18, 4) NULL, 
    [RefundMoney] DECIMAL(18, 4) NULL, 
    [OneToOneHourCount] DECIMAL(18, 4) NULL, 
    [OneToOneCampusRank] DECIMAL(18, 4) NULL, 
    [OneToOneBranchRank] DECIMAL(18, 4) NULL, 
    [OneToOneCountryRank] DECIMAL(18, 4) NULL, 
    [GroupHourCount] DECIMAL(18, 4) NULL, 
    [GroupCampusRank] DECIMAL(18, 4) NULL, 
    [GroupBranchRank] DECIMAL(18, 4) NULL, 
    [GroupCountryRank] DECIMAL(18, 4) NULL, 
    CONSTRAINT [PK_TeachStaffPerMonthSum] PRIMARY KEY ([Month], [StaffJobID], [Year]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学科组ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'教师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'HourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时量任务',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'HourCountTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneHourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量校区排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneCampusRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupHourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量校区排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupCampusRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachStaffPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupCountryRank'