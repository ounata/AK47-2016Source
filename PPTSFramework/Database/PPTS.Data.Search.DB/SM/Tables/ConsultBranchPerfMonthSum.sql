CREATE TABLE [SM].[ConsultBranchPerfMonthSum]
(
	[RegionID] NVARCHAR(36) NOT NULL, 
    [BranchID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [MoneyCount] DECIMAL(18, 4) NULL, 
    [MoneyRegionRank] INT NULL, 
    [MoneyCountryRank] INT NULL, 
    [PeopleCount] DECIMAL NULL, 
    [PeopleRegionRank] INT NULL, 
    [PeopleCountryRank] INT NULL, 
    [NewMoneyTask] DECIMAL(18, 4) NULL, 
    [NewMoney] DECIMAL(18, 4) NULL, 
    [NewMoneyRegionRank] INT NULL, 
    [NewMoneyCountryRank] INT NULL, 
    [NewPeopleCount] INT NULL, 
    [NewPeopleBranchRank] INT NULL, 
    [NewPeopleCountryRank] INT NULL, 
    [NewPeopleCountTask] INT NULL, 
    CONSTRAINT [PK_ConsultBranchPerfMonthSum] PRIMARY KEY NONCLUSTERED ([BranchID], [Year], [Month]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
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
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyCount'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'MoneyRegionRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleCount'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'PeopleRegionRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签任务金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoney'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'NewMoneyRegionRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCount'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询体系分公司按月业绩汇总表',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数任务数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCountTask'