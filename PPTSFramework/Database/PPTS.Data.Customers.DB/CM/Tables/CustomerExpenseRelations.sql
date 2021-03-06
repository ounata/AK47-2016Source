
CREATE TABLE [CM].[CustomerExpenseRelations](
	[CustomerID] [nvarchar](36) NOT NULL,
	[ExpenseID] [nvarchar](36) NOT NULL,
	[ExpenseType] [nvarchar](32) NULL,
	[ExpenseMoney] [decimal](18, 4) NULL,
	[AccountID] [nvarchar](36) NULL,
    [OrderID] NVARCHAR(36) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
 [TenantCode] NVARCHAR(64) NULL, 
    CONSTRAINT [PK_CustomerExpenseRelations] PRIMARY KEY NONCLUSTERED 
([CustomerID], [ExpenseID])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'ExpenseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用类型' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'ExpenseType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'费用金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'ExpenseMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联的账户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'AccountID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员服务费扣除关系表（如果返还，该记录删除）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerExpenseRelations'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'关联的订购单ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerExpenseRelations',
    @level2type = N'COLUMN',
    @level2name = N'OrderID'