
CREATE TABLE [CM].[AccountChargePayments](
	[ApplyID] [nvarchar](36) NOT NULL,
    [SortNo] INT NULL DEFAULT 0, 
	[PayID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[PayNo] [nvarchar](32) NOT NULL,
	[PayTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[PayType] [nvarchar](32) NOT NULL,
	[PayTicket] [nvarchar](64) NULL,
	[PayStatus] [nvarchar](32) NOT NULL,
	[PayMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[PayMemo] [nvarchar](255) NULL,
    [InputTime] DATETIME NULL, 
    [SwipeTime] DATETIME NULL, 
    [PrintStatus] NVARCHAR(32) NULL DEFAULT 0, 
	[Salesman] [nvarchar](64) NULL,
	[Payer] [nvarchar](64) NULL,
	[PayeeID] [nvarchar](36) NULL,
	[PayeeName] [nvarchar](64) NULL,
	[PayeeJobID] [nvarchar](36) NULL,
	[PayeeJobName] [nvarchar](64) NULL,
    [CheckerID] NVARCHAR(36) NULL, 
    [CheckerName] NVARCHAR(64) NULL, 
    [CheckerJobID] NVARCHAR(36) NULL, 
    [CheckerJobName] NVARCHAR(64) NULL, 
    [CheckStatus] NVARCHAR(32) NULL DEFAULT 0 , 
    [CheckTime] DATETIME NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL,
    [TenantCode] NVARCHAR(36) NULL, 
    [IsSyn] NVARCHAR(32) NULL DEFAULT 0, 
    [SynTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [PK_AccountChargePayments] PRIMARY KEY NONCLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], 
    CONSTRAINT [IX_AccountChargePayments] UNIQUE ([PayNo])
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountChargePayments]    Script Date: 2016/3/23 15:12:15 ******/


GO
/****** Object:  Index [IX_AccountChargePayments_1]    Script Date: 2016/3/23 15:12:15 ******/
CREATE NONCLUSTERED INDEX [IX_AccountChargePayments_1] ON [CM].[AccountChargePayments]
(
	[ApplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountChargePayments_2]    Script Date: 2016/3/23 15:12:15 ******/
ALTER TABLE [CM].[AccountChargePayments] ADD  CONSTRAINT [DF_AccountChargePayments_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [CM].[AccountChargePayments] ADD  CONSTRAINT [DF_AccountChargePayments_ModifyTime]  DEFAULT GETUTCDATE() FOR [ModifyTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'ApplyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付单号' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付时间（统一的付款时间）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付状态（0-未支付，1-已支付）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayStatus'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回款人' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'Salesman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款人（默认学员姓名）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'Payer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收款人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收款人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayeeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收款人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayeeJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收款人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayeeJobName'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户缴费支付单表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类型（现金，支票...）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name=N'PayMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'小票号' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePayments', @level2type=N'COLUMN',@level2name='PayTicket'
GO

GO

GO

GO

GO

GO

GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人岗位姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'打印状态（0-未打印，1-已打印）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = 'PrintStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'顺序号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'SortNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账状态（0-未对帐，1-已对账）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckStatus'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'刷卡时间（来自刷卡记录表）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'SwipeTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'输入时间（来自界面输入）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'InputTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否已同步（0未同步，1已同步,2无需同步）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'IsSyn'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'同步时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountChargePayments',
    @level2type = N'COLUMN',
    @level2name = N'SynTime'