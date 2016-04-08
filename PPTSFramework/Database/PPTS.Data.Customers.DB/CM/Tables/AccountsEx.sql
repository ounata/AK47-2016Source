
CREATE TABLE [CM].[AccountsEx](
	[AccountID] [nvarchar](36) NOT NULL,
	[ChargeMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[TransferInMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[TransferOutMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[RefundMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[IncomeMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[ExpenseMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NOT NULL DEFAULT getdate(), 
    [ModifierID] NVARCHAR(36) NULL, 
    [ModifierName] NVARCHAR(64) NULL, 
    [ModifyTime] DATETIME NOT NULL DEFAULT getdate(), 
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountsEx] PRIMARY KEY NONCLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountsEx_1]    Script Date: 2016/3/23 15:05:20 ******/


GO
/****** Object:  Index [IX_AccountsEx_2]    Script Date: 2016/3/23 15:05:20 ******/
ALTER TABLE [CM].[AccountsEx] ADD  CONSTRAINT [DF_AccountsEx_AccountID]  DEFAULT (newid()) FOR [AccountID]
GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx', @level2type=N'COLUMN',@level2name=N'AccountID'
GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计充值金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx', @level2type=N'COLUMN',@level2name=N'ChargeMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计转入金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx', @level2type=N'COLUMN',@level2name=N'TransferInMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计转出金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx', @level2type=N'COLUMN',@level2name=N'TransferOutMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计退款金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx', @level2type=N'COLUMN',@level2name=N'RefundMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认收入总额（包含服务费）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx', @level2type=N'COLUMN',@level2name=N'IncomeMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务费总额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx', @level2type=N'COLUMN',@level2name=N'ExpenseMoney'
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

GO

GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户信息扩展表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountsEx'
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountsEx',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountsEx',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountsEx',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountsEx',
    @level2type = N'COLUMN',
    @level2name = N'ModifierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountsEx',
    @level2type = N'COLUMN',
    @level2name = N'ModifierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后修改时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountsEx',
    @level2type = N'COLUMN',
    @level2name = N'ModifyTime'
GO
