CREATE TABLE [SM].[ConsultBranchCrmDaySum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [Day] INT NOT NULL, 
    [VisitingCountOfPotential] INT NULL, 
    [VisitingCountOfStudent] INT NULL, 
    [VerifyingCount] INT NULL, 
    [SigningCount] INT NULL, 
    CONSTRAINT [PK_ConsultBranchCrmDaySum] PRIMARY KEY NONCLUSTERED ([BranchID], [Year], [Month], [Day]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Day'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日需要回访人数（潜客）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'VisitingCountOfPotential'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日需要回访人数（学员）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'VisitingCountOfStudent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日预计上门人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'VerifyingCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当日预计签约人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = N'COLUMN',
    @level2name = 'SigningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询体系咨询师按天跟进汇总表',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmDaySum',
    @level2type = NULL,
    @level2name = NULL
GO

GO

GO

GO
