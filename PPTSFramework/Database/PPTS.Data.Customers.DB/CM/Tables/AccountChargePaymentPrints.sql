

CREATE TABLE [CM].[AccountChargePaymentPrints](
	[PayID] [nvarchar](36) NOT NULL,
	[PrintID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[PrintTime] [datetime] NOT NULL DEFAULT getdate(),
	[PrintorID] [nvarchar](36) NULL,
	[PrintorName] [nvarchar](64) NULL,
	[PrintMemo] [nvarchar](255) NULL,
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountChargePaymentPrints] PRIMARY KEY NONCLUSTERED 
(
	[PrintID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountChargePaymentPrints]    Script Date: 2016/3/23 15:11:29 ******/
CREATE NONCLUSTERED INDEX [IX_AccountChargePaymentPrints_1] ON [CM].[AccountChargePaymentPrints]
(
	[PayID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePaymentPrints', @level2type=N'COLUMN',@level2name=N'PayID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePaymentPrints', @level2type=N'COLUMN',@level2name=N'PrintID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePaymentPrints', @level2type=N'COLUMN',@level2name=N'PrintTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePaymentPrints', @level2type=N'COLUMN',@level2name=N'PrintorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePaymentPrints', @level2type=N'COLUMN',@level2name=N'PrintorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打印说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePaymentPrints', @level2type=N'COLUMN',@level2name=N'PrintMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户缴费支付单打印表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountChargePaymentPrints'
GO
