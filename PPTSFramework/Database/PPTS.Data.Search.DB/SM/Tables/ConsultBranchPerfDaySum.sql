CREATE TABLE [SM].[ConsultBranchPerfDaySum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [Day] INT NOT NULL, 
    [MoneyCount] DECIMAL(18, 4) NULL, 
    [PeopleCount] INT NULL, 
    [NewMoneyCount] DECIMAL(18, 4) NULL, 
    [NewPeopleCount] INT NULL, 
    CONSTRAINT [PK_ConsultBranchPerfDaySum] PRIMARY KEY NONCLUSTERED ([BranchID], [Year], [Month], [Day]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
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
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Day'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询体系分公司按天业绩汇总表',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfDaySum',
    @level2type = NULL,
    @level2name = NULL