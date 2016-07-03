CREATE TABLE [SM].[TeachCountryPerMonthSum]
(
    [Year] INT NOT NULL, 
    [Month] INT NOT NULL, 
    [HourCount] DECIMAL(18, 4) NULL, 
    [HourCountTask] DECIMAL(18, 4) NULL, 
    [RenewalMoney] DECIMAL(18, 4) NULL, 
    [RefundMoney] DECIMAL(18, 4) NULL, 
    [OneToOneHourCount] DECIMAL(18, 4) NULL, 
    [GroupHourCount] DECIMAL(18, 4) NULL, 
    CONSTRAINT [PK_TeachCountryPerMonthSum] PRIMARY KEY ([Year], [Month]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'年份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Year'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'月份',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'Month'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'HourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课时量任务',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'HourCountTask'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'续费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RenewalMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'退费金额（元）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'RefundMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1对1课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'OneToOneHourCount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'班组课时量（个）',
    @level0type = N'SCHEMA',
    @level0name = N'SM',
    @level1type = N'TABLE',
    @level1name = N'TeachCountryPerMonthSum',
    @level2type = N'COLUMN',
    @level2name = N'GroupHourCount'