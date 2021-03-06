
CREATE TABLE [PM].[ProductSalaryRules](
	[RuleID] [nvarchar](36) NOT NULL,
	[ProductID] [nvarchar](36) NOT NULL,
	[RuleObject] [nvarchar](32) NOT NULL,
    [MoneyPerHour] DECIMAL NULL DEFAULT 0, 
    [MoneyPerPeriod] DECIMAL(18, 4) NULL DEFAULT 0, 
	[TenantCode] [nvarchar](36) NULL,
    CONSTRAINT [PK_ProductSalaryRules] PRIMARY KEY NONCLUSTERED 
(
	[RuleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductSalaryRules] UNIQUE NONCLUSTERED 
(
	[ProductID] ASC,
	[RuleObject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [PM].[ProductSalaryRules] ADD  CONSTRAINT [DF_ProductSalaryRules_RuleID]  DEFAULT newid() FOR [RuleID]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'薪酬规则ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductSalaryRules', @level2type=N'COLUMN',@level2name=N'RuleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产品ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductSalaryRules', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规则对象枚举（咨询师，学管师，教师）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'ProductSalaryRules', @level2type=N'COLUMN',@level2name=N'RuleObject'
GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'薪酬规则表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductSalaryRules',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课酬金额每课时',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductSalaryRules',
    @level2type = N'COLUMN',
    @level2name = 'MoneyPerPeriod'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'课酬金额每小时',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'ProductSalaryRules',
    @level2type = N'COLUMN',
    @level2name = N'MoneyPerHour'