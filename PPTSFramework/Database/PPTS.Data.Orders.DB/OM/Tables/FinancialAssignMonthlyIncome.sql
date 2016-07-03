CREATE TABLE [OM].[FinancialAssignMonthlyIncome]
(
	[CheckYear] INT NOT NULL, 
    [CheckMonth] INT NOT NULL, 
    [BranchID] NVARCHAR(72) NULL, 
    [BranchName] NVARCHAR(256) NULL, 
    [CampusID] NVARCHAR(72) NOT NULL, 
    [CampusName] NVARCHAR(256) NULL, 
    [CategoryType] NVARCHAR(64) NOT NULL, 
    [CategoryName] NVARCHAR(128) NULL, 
    [Catalog] NVARCHAR(64) NOT NULL, 
    [CatalogName] NVARCHAR(128) NULL, 
    [Amount] DECIMAL(18, 4) NULL, 
    [TaxAmount] DECIMAL(18, 4) NULL, 
    [AllAmount] DECIMAL(18, 4) NULL, 
    [TaxRate] DECIMAL(18, 4) NULL, 
    [IsSyn] NVARCHAR(32) NULL DEFAULT 0, 
    [SynTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [CreateTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [ModifyTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [PK_FinancialAssignMonthlyIncome] PRIMARY KEY ([CheckYear], [CheckMonth], [CampusID], [CategoryType], [Catalog]) 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账年度',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CheckYear'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账月份',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CheckMonth'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'BranchID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'BranchName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CategoryType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品类型名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CategoryName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品分类代码',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'Catalog'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'产品分类名称',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CatalogName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'金额',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'Amount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'税额',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'TaxAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'价税合计',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'AllAmount'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'税率',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'TaxRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否已同步（0未同步，1已同步,2无需同步）',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'IsSyn'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'同步时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'SynTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'OM',
    @level1type = N'TABLE',
    @level1name = N'FinancialAssignMonthlyIncome',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'