CREATE TABLE [SM].[ConsultStaffCrmDaySum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NULL, 
    [CampusID] NVARCHAR(36) NULL, 
    [StaffID] NVARCHAR(36) NOT NULL, 
    [StaffJobID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [Day] INT NOT NULL, 
    [VisitingCountOfPotential] INT NULL, 
    [VisitingCountOfStudent] INT NULL, 
    [VerifyingCount] INT NULL, 
    [SigningCount] INT NULL, 
    [WeekVisitingCountOfPotential] INT NULL, 
    [WeekVisitingCountOfStudent] INT NULL, 
    [WeekVerifyingCount] INT NULL, 
    [WeekSigningCount] INT NULL, 
    CONSTRAINT [PK_ConsultStaffCrmDaySum] PRIMARY KEY NONCLUSTERED ([StaffJobID], [Year], [Month], [Day]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Day'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日需要回访人数（潜客）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'VisitingCountOfPotential'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日需要回访人数（学员）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'VisitingCountOfStudent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日预计上门人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'VerifyingCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日预计签约人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'SigningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询体系咨询师按天跟进汇总表',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本周当日需要回访人数（潜客）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'WeekVisitingCountOfPotential'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本周当日需要回访人数（学员）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'WeekVisitingCountOfStudent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本周当日预计上门人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'WeekVerifyingCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本周当日预计签约人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'WeekSigningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'