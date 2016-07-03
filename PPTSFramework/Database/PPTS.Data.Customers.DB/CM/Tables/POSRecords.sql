CREATE TABLE [CM].[POSRecords]
(
	[TransactionDate] DATETIME NULL DEFAULT getutcdate() , 
    [SettlementDate] DATETIME NULL DEFAULT getutcdate(), 
	[TransactionTimeValue] [nvarchar](64) NULL,
    [TransactionTime] DATETIME NULL DEFAULT getutcdate(), 
    [TransactionType] NVARCHAR(32) NULL, 
    [TransactionID] NVARCHAR(36) NOT NULL, 
    [CardNum] NVARCHAR(32) NULL, 
    [MerchantID] NVARCHAR(36) NOT NULL, 
    [POSID] NVARCHAR(36) NOT NULL, 
    [Money] DECIMAL(18, 4) NULL, 
    [FromType] NVARCHAR(32) NULL,
	[IsUsered] bit NULL DEFAULT 0,
    [CreateTime] DATETIME NULL DEFAULT getutcdate(), 
    PRIMARY KEY CLUSTERED ([TransactionID], [POSID], [MerchantID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'交易日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'TransactionDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'清算日期',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'SettlementDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'刷卡交易时间-(交易日期+交易时间)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'TransactionTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'交易参考号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'TransactionID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'银行卡号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'CardNum'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID（商户号）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'MerchantID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'终端号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'POSID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'刷卡金额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'Money'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'来源类型(1--接口(实时接口)来源、2--对账(异步接口)来源)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'FromType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'银联刷卡记录',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否核销',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'IsUsered'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'刷卡交易时间(来源原始值)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'TransactionTimeValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'刷卡交易类型（1-银联，4-通联）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'POSRecords',
    @level2type = N'COLUMN',
    @level2name = N'TransactionType'