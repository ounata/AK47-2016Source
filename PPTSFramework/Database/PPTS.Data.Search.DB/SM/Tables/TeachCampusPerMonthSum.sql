CREATE TABLE [SM].[TeachCampusPerMonthSum]
(
	[RegionID] NVARCHAR(36) NULL, 
    [BranchID] NVARCHAR(36) NULL, 
    [CampusID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [HourCount] DECIMAL(18, 4) NULL, 
    [HourCountTask] DECIMAL(18, 4) NULL, 
    [RenewalMoney] DECIMAL(18, 4) NULL, 
    [RefundMoney] DECIMAL(18, 4) NULL, 
    [OneToOneHourCount] DECIMAL(18, 4) NULL, 
    [OneToOneBranchRank] DECIMAL(18, 4) NULL, 
    [OneToOneCountryRank] DECIMAL(18, 4) NULL, 
    [GroupHourCount] DECIMAL(18, 4) NULL, 
    [GroupBranchRank] DECIMAL(18, 4) NULL, 
    [GroupCountryRank] DECIMAL(18, 4) NULL, 
    CONSTRAINT [PK_TeachCampusPerMonthSum] PRIMARY KEY ([CampusID], [Month], [Year]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'区域ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分工司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'HourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时量任务',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'HourCountTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneHourCount'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCampusPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupHourCount'