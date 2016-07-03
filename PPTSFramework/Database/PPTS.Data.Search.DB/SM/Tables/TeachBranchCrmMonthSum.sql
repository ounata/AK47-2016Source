CREATE TABLE [SM].[TeachBranchCrmMonthSum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [ClassPeopleCount] INT NULL, 
    [InitialEffective] INT NULL, 
    [NewEffective] INT NULL, 
    [ClassHours] DECIMAL(18, 4) NULL, 
    [OutSchoolWarningCount] INT NULL, 
    [ClosedWarningCount] INT NULL, 
    [RefundWarningCount] INT NULL, 
    [ClassNoSatisfied ] INT NULL, 
    [ScoreNoSatisfied ] INT NULL, 
    [NewStudentCount] INT NULL, 
    [WaitMeetingCount] INT NULL, 
    [BirthdayToday] INT NULL, 
    [BirthdayFuture] INT NULL, 
    [ManageStudentCount] INT NULL, 
    [ValidStudentCount] INT NULL, 
    [ClosedStudentCount] INT NULL, 
    [OutSchoolStudentCount] INT NULL, 
    [RenewalCount] INT NULL, 
    [EndClassCount] INT NULL, 
    [RefundCount] INT NULL, 
    [IntroduceCount] INT NULL, 
    [SwitchCount] INT NULL, 
    [ReceiveCount] INT NULL, 
    [NewClosedCount] INT NULL, 
    [ClosedActivationCount] INT NULL, 
    [NewOutSchoolCount] INT NULL, 
    [OutSchoolActivationCount] INT NULL, 
    CONSTRAINT [PK_TeachBranchCrmMonthSum] PRIMARY KEY ([BranchID], [Month], [Year]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上课人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClassPeopleCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'期初有效',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'InitialEffective'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新增有效',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewEffective'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上课小时数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClassHours'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'休学预警',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OutSchoolWarningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'停课预警',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClosedWarningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费预警',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundWarningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课后不满意',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClassNoSatisfied '
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'成绩不满意',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ScoreNoSatisfied '
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新生人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'待开家长会的学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'WaitMeetingCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'生日提醒-今日生日',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BirthdayToday'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'生日提醒-未来15天内生日',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BirthdayFuture'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'管理学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ManageStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'有效学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ValidStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'停课学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClosedStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'休学学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OutSchoolStudentCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'结课学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'EndClassCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转介绍学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'IntroduceCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月接档学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ReceiveCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月调换学员',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'SwitchCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月新增停课',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewClosedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月停课激活',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'ClosedActivationCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月新增休学',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewOutSchoolCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月休学激活',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OutSchoolActivationCount'