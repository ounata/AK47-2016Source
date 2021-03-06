
CREATE TABLE [CM].[Accounts](
	[CustomerID] [nvarchar](36) NOT NULL,
	[AccountID] [nvarchar](36) NOT NULL,
	[AccountCode] [nvarchar](32) NOT NULL,
	[AccountType] [nvarchar](32) NOT NULL DEFAULT '1',
	[AccountMemo] [nvarchar](255) NULL,
	[AccountStatus] [nvarchar](32) NOT NULL DEFAULT '1',
	[AccountMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[DiscountID] [nvarchar](36) NULL,
	[DiscountCode] [nvarchar](64) NULL,
	[DiscountRate] [decimal](18, 4) NOT NULL DEFAULT 1,
	[DiscountBase] [decimal](18, 4) NOT NULL DEFAULT 0,
    [ChargeApplyID] NVARCHAR(36) NULL, 
    [ChargePayTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    [FirstChargePayTime] DATETIME NULL, 
    [FirstChargeApplyID] NVARCHAR(36) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL,
	[VersionStartTime] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[VersionEndTime] DATETIME NULL DEFAULT '99990909 00:00:00' ,
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_Accounts] PRIMARY KEY NONCLUSTERED 
([AccountID], [VersionStartTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_Accounts_1]    Script Date: 2016/3/23 15:05:20 ******/


GO
/****** Object:  Index [IX_Accounts_2]    Script Date: 2016/3/23 15:05:20 ******/
ALTER TABLE [CM].[Accounts] ADD  CONSTRAINT [DF_Accounts_AccountID]  DEFAULT newid() FOR [AccountID]
GO
ALTER TABLE [CM].[Accounts] ADD  CONSTRAINT [DF_Accounts_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [CM].[Accounts] ADD  CONSTRAINT [DF_Accounts_ModifyTime]  DEFAULT GETUTCDATE() FOR [ModifyTime]
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'AccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户编码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'AccountCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户类型（0- 合同账户，1-非合同账户【拓路账户】）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'AccountType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'AccountMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户状态（0-不可充值，1-可充值）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'AccountStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户余额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'AccountMoney'
GO

GO

GO

GO

GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'DiscountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣基数' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'DiscountBase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣编码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'DiscountCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣率' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'DiscountRate'
GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户信息表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'Accounts'
GO



GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最新缴费申请单ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Accounts',
    @level2type = N'COLUMN',
    @level2name = N'ChargeApplyID'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Accounts',
    @level2type = N'COLUMN',
    @level2name = N'VersionStartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'版本结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Accounts',
    @level2type = N'COLUMN',
    @level2name = N'VersionEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最新缴费单支付日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Accounts',
    @level2type = N'COLUMN',
    @level2name = N'ChargePayTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次缴费单支付日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Accounts',
    @level2type = N'COLUMN',
    @level2name = N'FirstChargePayTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'首次缴费申请单ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'Accounts',
    @level2type = N'COLUMN',
    @level2name = N'FirstChargeApplyID'