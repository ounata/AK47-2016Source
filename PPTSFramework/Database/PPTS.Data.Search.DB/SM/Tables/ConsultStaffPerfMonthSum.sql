CREATE TABLE [SM].[ConsultStaffPerfMonthSum]
(
	[RegionID] NVARCHAR(36) NOT NULL, 
    [BranchID] NVARCHAR(36) NULL, 
    [CampusID] NVARCHAR(36) NULL, 
    [StaffID] NVARCHAR(36) NOT NULL, 
    [StaffJobID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [MoneyCount] DECIMAL(18, 4) NULL, 
    [MoneyCampusRank] INT NULL, 
    [MoneyBranchRank] INT NULL, 
    [MoneyCountryRank] INT NULL, 
    [PeopleCount] DECIMAL NULL, 
    [PeopleCampusRank] INT NULL, 
    [PeopleBranchRank] INT NULL, 
    [PeopleCountryRank] INT NULL, 
    [NewMoneyTask] DECIMAL(18, 4) NULL, 
    [NewMoney] DECIMAL(18, 4) NULL, 
    [NewMoneyCampusRank] INT NULL, 
    [NewMoneyBranchRank] INT NULL, 
    [NewMoneyCountryRank] INT NULL, 
    [NewPeopleCount] INT NULL, 
    [NewPeopleCampusRank] INT NULL, 
    [NewPeopleBranchRank] INT NULL, 
    [NewPeopleCountryRank] INT NULL, 
    CONSTRAINT [PK_ConsultStaffPerfMonthSum] PRIMARY KEY NONCLUSTERED ([StaffJobID], [Year], [Month]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'StaffID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额校区排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyCampusRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数校区排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleCampusRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签任务金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额校区排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyCampusRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数校区排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCampusRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数分公司排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleBranchRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数全国排名',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCountryRank'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询体系咨询师按月业绩汇总表',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询师岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultStaffPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'StaffJobID'