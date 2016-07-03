﻿CREATE TABLE [SM].[EducateStaffCrmMonthSum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NULL, 
    [CampusID] NVARCHAR(36) NULL, 
    [StaffID] NVARCHAR(36) NULL, 
    [StaffJobID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [ClassPeopleCount] INT NULL, 
    [InitialEffective] INT NULL, 
    [NewEffective] INT NULL, 
    [ClassHours] INT NULL, 
    [FinalEffective] INT NULL, 
    [RenewalCount] INT NULL, 
    [EndClassCount] INT NULL, 
    [RefundCount] INT NULL, 
    [RemainingHoursCount] INT NULL, 
    [OutSchoolWarningCount] INT NULL, 
    [ClosedWarningCount] INT NULL, 
    [RefundWarningCount] INT NULL, 
    [WaitVisitingCount] INT NULL, 
    [WaitWeekFeedbackCount] INT NULL, 
    [WaitMeetingCount] INT NULL, 
    [WaitOrderCount] INT NULL, 
    [BirthdayToday] INT NULL, 
    [BirthdayFuture] INT NULL, 
    [ManageStudentCount] INT NULL, 
    [ValidStudentCount] INT NULL, 
    [NoClassStudentCount] INT NULL, 
    [ClosedStudentCount] INT NULL, 
    [OutSchoolStudentCount] INT NULL, 
    [OnlyStudentCount] INT NULL, 
    [NoOrderStudentCount] INT NULL, 
    [IntroduceCount] INT NULL, 
    [SwitchCount] INT NULL, 
    [ReceiveCount] INT NULL, 
    [NewClosedCount] INT NULL, 
    [ClosedActivationCount] INT NULL, 
    [NewOutSchoolCount] INT NULL, 
    [OutSchoolActivationCount] INT NULL, 
    CONSTRAINT [PK_EducateStaffCrmMonthSum] PRIMARY KEY ([StaffJobID], [Month], [Year]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月休学激活',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OutSchoolActivationCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月新增休学',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewOutSchoolCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月停课激活',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClosedActivationCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月新增停课',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewClosedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月接档学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ReceiveCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月调换学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'SwitchCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'IntroduceCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'无订单学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NoOrderStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'单科学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OnlyStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'休学学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OutSchoolStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'停课学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClosedStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签未上课学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NoClassStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'有效学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ValidStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'管理学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ManageStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'生日提醒-未来15天内生日',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BirthdayFuture'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'生日提醒-今日生日',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BirthdayToday'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'待订购',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'WaitOrderCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'待开家长会的学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'WaitMeetingCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'待周反馈的学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'WaitWeekFeedbackCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'待回访的学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'WaitVisitingCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费预警',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundWarningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'停课预警',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClosedWarningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'休学预警',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OutSchoolWarningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'剩余课时小于20的学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RemainingHoursCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'结课人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'EndClassCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'期末有效',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'FinalEffective'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上课小时数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClassHours'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新增有效',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewEffective'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'期初有效',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'InitialEffective'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上课人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClassPeopleCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学管师ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateStaffCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'