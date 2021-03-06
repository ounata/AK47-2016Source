
CREATE TABLE [CM].[AccountChargeInvoices](
	[ApplyID] [nvarchar](36) NOT NULL,
	[InvoiceID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[InvoiceNo] [nvarchar](64) NULL,
	[InvoiceMoney] [decimal](18, 4) NULL DEFAULT 0,
	[InvoiceClauses] [nvarchar](MAX) NULL,
	[InvoiceHeader] [nvarchar](255) NULL,
	[InvoiceTime] [datetime] NULL,
	[InvoiceMemo] [nvarchar](255) NULL,
	[InvoiceStatus] [nvarchar](32) NOT NULL,
	[IsDiscarded] NVARCHAR(32) NULL,
	[ReturnTime] [datetime] NULL,
	[ReturnMemo] [nvarchar](255) NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL DEFAULT getdate(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT getdate(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountChargeInvoices] PRIMARY KEY NONCLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountChargeInvoices]    Script Date: 2016/3/23 15:09:51 ******/
CREATE NONCLUSTERED INDEX [IX_AccountChargeInvoices_1] ON [CM].[AccountChargeInvoices]
(
	[ApplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'ApplyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票号' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票条目' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceClauses'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票抬头' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceHeader'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开票时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开票状态（1-正常，2-已退）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'InvoiceStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否作废（1-正常，2-作废）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name='IsDiscarded'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退票时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'ReturnTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退票说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'ReturnMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户缴费发票记录表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargeInvoices'
GO
