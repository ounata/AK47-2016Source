
CREATE TABLE [CM].[AccountRecords](
	[CampusID] [nvarchar](36) NOT NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[AccountID] [nvarchar](36) NOT NULL,
	[RecordID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[RecordTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[RecordType] [nvarchar](32) NOT NULL,
	[RecordFlag] [int] NOT NULL DEFAULT 1,
	[BillID] [nvarchar](36) NOT NULL,
	[BillNo] [nvarchar](64) NOT NULL,
	[BillRelateID] [nvarchar](36) NOT NULL,
    [BillRelateNo] NVARCHAR(64) NULL, 
	[BillTime] [datetime] NOT NULL,
	[BillType] [nvarchar](32) NOT NULL,
	[BillTypeName] [nvarchar](64) NULL,
	[BillMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[BillMemo] [nvarchar](MAX) NULL,
	[BillerID] [nvarchar](36) NULL,
	[BillerName] [nvarchar](64) NULL,
	[BillerJobID] [nvarchar](36) NULL,
	[BillerJobName] [nvarchar](64) NULL,
    [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountRecords] PRIMARY KEY NONCLUSTERED 
(
	[RecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountRecords]    Script Date: 2016/3/23 15:03:34 ******/


GO
/****** Object:  Index [IX_AccountRecords_1]    Script Date: 2016/3/23 15:03:34 ******/
CREATE NONCLUSTERED INDEX [IX_AccountRecords_2] ON [CM].[AccountRecords]
([CustomerID], [RecordTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO
/****** Object:  Index [IX_AccountRecords_2]    Script Date: 2016/3/23 15:03:34 ******/
CREATE NONCLUSTERED INDEX [IX_AccountRecords_3] ON [CM].[AccountRecords]
([AccountID], [RecordTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountRecords_3]    Script Date: 2016/3/23 15:03:34 ******/
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'AccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'RecordID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'RecordTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水类型（1充值，2退费,3 转入, 4转出，5订购，6退订, 7服务费扣除，8服务费返还）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'RecordType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水方向（1是入，-1是出）  ' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'RecordFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'BillID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务单号' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'BillNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务操作时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'BillTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务类型' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'BillType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务类型描述' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name='BillTypeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'BillMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name=N'BillMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务操作人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name='BillerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务操作人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name='BillerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务操作人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name='BillerJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务操作人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords', @level2type=N'COLUMN',@level2name='BillerJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户流水记录表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRecords'
GO


CREATE INDEX [IX_AccountRecords_4] ON [CM].[AccountRecords] ([RecordTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务关联单号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRecords',
    @level2type = N'COLUMN',
    @level2name = N'BillRelateNo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'业务关联ID（例如退订单关联订购单）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRecords',
    @level2type = N'COLUMN',
    @level2name = N'BillRelateID'