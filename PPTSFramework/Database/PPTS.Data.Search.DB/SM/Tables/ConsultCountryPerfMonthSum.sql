CREATE TABLE [SM].[ConsultCountryPerfMonthSum]
(
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [MoneyCount] DECIMAL(18, 4) NULL, 
    [PeopleCount] DECIMAL NULL, 
    [NewMoneyTask] DECIMAL(18, 4) NULL, 
    [NewMoney] DECIMAL(18, 4) NULL, 
    [NewPeopleCount] INT NULL, 
    [NewPeopleCountTask] INT NULL, 
    CONSTRAINT [PK_ConsultCountryPerfMonthSum] PRIMARY KEY NONCLUSTERED ([Year], [Month]) 
)

GO

GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'MoneyCount'
GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'前期人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'PeopleCount'
GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签任务金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoneyTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签金额',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewMoney'
GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCount'
GO

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'咨询体系全国按月业绩汇总表',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'新签人数任务数',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'ConsultCountryPerfMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'NewPeopleCountTask'