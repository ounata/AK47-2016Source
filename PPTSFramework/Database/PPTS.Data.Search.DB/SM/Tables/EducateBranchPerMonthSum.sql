CREATE TABLE [SM].[EducateBranchPerMonthSum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [OneToOneHourCount] INT NULL, 
    [OneToOneHourIncome] DECIMAL(18, 4) NULL, 
    [OneToOneBranchRank] INT NULL, 
    [OneToOneCountryRank] INT NULL, 
    [GroupHourCount] INT NULL, 
    [GroupHourIncome] DECIMAL(18, 4) NULL, 
    [GroupBranchRank] INT NULL, 
    [GroupCountryRank] INT NULL, 
    [RenewalMoney] DECIMAL(18, 4) NULL, 
    [RenewalBranchRank] INT NULL, 
    [RenewalCountryRank] INT NULL, 
    [RefundMoney] DECIMAL(18, 4) NULL, 
    [HourCountTask] INT NULL, 
    [RenewalMoneyTask] DECIMAL(18, 4) NULL, 
    CONSTRAINT [PK_EducateBranchPerMonthSum] PRIMARY KEY ([BranchID], [Month], [Year]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费金额任务',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalMoneyTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时量任务',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'HourCountTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时收入全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时收入分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时收入（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupHourIncome'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupHourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时收入全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时收入分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时收入（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneHourIncome'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneHourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateBranchPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'