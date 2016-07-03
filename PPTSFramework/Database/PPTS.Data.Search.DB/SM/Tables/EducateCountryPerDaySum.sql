CREATE TABLE [SM].[EducateCountryPerDaySum]
(
	[Year] INT NOT NULL , 
    [Month] INT NOT NULL, 
    [Day] INT NOT NULL, 
    [OneToOneHourCount] INT NULL, 
    [OneToOneHourIncome] DECIMAL(18, 4) NULL, 
    [GroupHourCount] INT NULL, 
    [GroupHourIncome] DECIMAL(18, 4) NULL, 
    [RenewalMoney] DECIMAL(18, 4) NULL, 
    [RefundMoney] DECIMAL(18, 4) NULL, 
    CONSTRAINT [PK_EducateCountryPerDaySum] PRIMARY KEY ([Year], [Day], [Month])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'天',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'Day'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneHourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时收入（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneHourIncome'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'GroupHourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时收入（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'GroupHourIncome'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'EducateCountryPerDaySum',
    @level2type = N'COLUMN',
    @level2name = N'RefundMoney'