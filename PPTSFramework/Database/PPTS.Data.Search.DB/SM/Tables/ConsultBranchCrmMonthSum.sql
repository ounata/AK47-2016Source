CREATE TABLE [SM].[ConsultBranchCrmMonthSum]
(
	[RegionID] NVARCHAR(36) NOT NULL, 
    [BranchID] NVARCHAR(36) NOT NULL, 
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [VisitingCountOfPotential] INT NULL, 
    [VisitingCountOfStudent] INT NULL, 
    [VerifyingCount] INT NULL, 
    [SigningCount] INT NULL, 
    [UnVisitCountOfPotential] INT NULL, 
    [UnVisitCountOfStudent] INT NULL, 
    [UnVerifyCount] INT NULL, 
    [UnSignCount] INT NULL, 
    [VisitedCountOfPotential] INT NULL, 
    [VisitedCountOfStudent] INT NULL, 
    [VerifiedCount] INT NULL, 
    [SignedCount] INT NULL, 
    [NewPotentialCount] INT NULL, 
    [ConvertedRatio] DECIMAL(18, 4) NULL, 
    [VerifiedRatio] DECIMAL(18, 4) NULL, 
    [SignedRatio] DECIMAL(18, 4) NULL, 
    CONSTRAINT [PK_ConsultBranchCrmMonthSum] PRIMARY KEY NONCLUSTERED ([BranchID], [Year], [Month]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'大区ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RegionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
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
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'需要回访人数（学员）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'VisitingCountOfStudent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预计上门人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'VerifyingCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预计签约人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'SigningCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月至当前未回访人数（潜客）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'UnVisitCountOfPotential'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月至当前未回访人数（学员）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'UnVisitCountOfStudent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月至当前未上门人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'UnVerifyCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'本月至当前未签约人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'UnSignCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实际回访人数（潜客）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'VisitedCountOfPotential'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实际回访人数（学员）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'VisitedCountOfStudent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实际上门人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'VerifiedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'实际签约人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'SignedCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新增潜客资源数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'NewPotentialCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'上门率',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'VerifiedRatio'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'签约率',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'SignedRatio'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转化率',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = 'ConvertedRatio'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询体系咨询师按月业绩汇总表',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'需要回访人数（潜客）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultBranchCrmMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'VisitingCountOfPotential'