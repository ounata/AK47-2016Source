

CREATE TABLE [CM].[AccountRefundPayments](
	[ApplyID] [nvarchar](36) NOT NULL,
	[PayID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[PayNo] [nvarchar](32) NOT NULL,
	[PayTime] [datetime] NOT NULL DEFAULT getdate(),
	[PayMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[PayStatus] [nvarchar](32) NOT NULL,
	[PayMemo] [nvarchar](255) NULL,
	[PayerID] [nvarchar](36) NULL,
	[PayerName] [nvarchar](50) NULL,
	[PayerJobID] [nvarchar](36) NULL,
	[PayerJobName] [nvarchar](50) NULL,
    [Checked] INT NULL DEFAULT 0, 
    [CheckTime] DATETIME NULL, 
    [CheckerID] NVARCHAR(36) NULL, 
    [CheckerName] NVARCHAR(64) NULL, 
    [CheckerJobID] NVARCHAR(36) NULL, 
    [CheckerJobName] NVARCHAR(64) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL,
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountRefundPayments] PRIMARY KEY NONCLUSTERED 
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AccountRefundPayments] UNIQUE NONCLUSTERED 
(
	[PayNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountRefundPayments_1]    Script Date: 2016/3/23 15:30:22 ******/
CREATE NONCLUSTERED INDEX [IX_AccountRefundPayments_1] ON [CM].[AccountRefundPayments]
(
	[ApplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountRefundPayments_2]    Script Date: 2016/3/23 15:30:22 ******/
ALTER TABLE [CM].[AccountRefundPayments] ADD  CONSTRAINT [DF_AccountRefundPayments_CreateTime]  DEFAULT getdate() FOR [CreateTime]
GO
ALTER TABLE [CM].[AccountRefundPayments] ADD  CONSTRAINT [DF_AccountRefundPayments_ModifyTime]  DEFAULT getdate() FOR [ModifyTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'ApplyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付单号' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付状态（参考缴费单）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayMemo'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayerJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'PayerJobName'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundPayments', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'支付金额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundPayments',
    @level2type = N'COLUMN',
    @level2name = N'PayMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户退费支付表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundPayments',
    @level2type = NULL,
    @level2name = NULL
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundPayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundPayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundPayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundPayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对账人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundPayments',
    @level2type = N'COLUMN',
    @level2name = N'CheckerJobName'