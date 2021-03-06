

CREATE TABLE [CM].[AccountTransferApplies](
	[ApplyID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[ApplyNo] [nvarchar](32) NOT NULL,
	[ApplyStatus] [nvarchar](32) NOT NULL,
	[ApplyMemo] [nvarchar](MAX) NULL,
	[ApplyTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ApplierID] [nvarchar](36) NULL,
	[ApplierName] [nvarchar](64) NULL,
	[ApplierJobID] [nvarchar](36) NULL,
	[ApplierJobName] [nvarchar](64) NULL,
    [ProcessStatus] NVARCHAR(32) NULL DEFAULT '0' , 
    [ProcessTime] DATETIME NULL, 
    [ProcessMemo] NVARCHAR(255) NULL, 
	[TransferType] NVARCHAR(32) NOT NULL DEFAULT 1 , 
	[TransferMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
	[SubmitterID] [nvarchar](36) NULL,
	[SubmitterName] [nvarchar](64) NULL,
	[SubmitterJobID] [nvarchar](36) NULL,
	[SubmitterJobName] [nvarchar](64) NOT NULL,
	[SubmitTime] [datetime] NULL,
    [ApproverID] NVARCHAR(36) NULL, 
    [ApproverName] NVARCHAR(64) NULL, 
    [ApproverJobID] NVARCHAR(36) NULL, 
    [ApproverJobName] NVARCHAR(64) NULL, 
    [ApproveTime] DATETIME NULL, 
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[CustomerCode] [nvarchar](64) NULL,
	[CustomerName] [nvarchar](128) NULL,
	[AccountID] [nvarchar](36) NOT NULL,
	[AccountCode] [nvarchar](64) NULL,
    [AccountType] NVARCHAR(32) NULL, 
    [ThatDiscountID] NVARCHAR(36) NULL, 
    [ThatDiscountCode] NVARCHAR(64) NULL, 
    [ThatDiscountBase] DECIMAL(18, 4) NULL, 
    [ThatDiscountRate] DECIMAL(18, 2) NULL DEFAULT 1, 
    [ThatAccountValue] DECIMAL(18, 4) NULL, 
    [ThatAccountMoney] DECIMAL(18, 4) NULL, 
    [ThisDiscountID] NVARCHAR(36) NULL, 
    [ThisDiscountCode] NVARCHAR(64) NULL, 
    [ThisDiscountBase] DECIMAL(18, 4) NULL, 
    [ThisDiscountRate] DECIMAL(18, 2) NULL, 
    [ThisAccountValue] DECIMAL(18, 4) NULL, 
    [ThisAccountMoney] DECIMAL(18, 4) NULL, 
	[BizCampusID] [nvarchar](36) NOT NULL,
	[BizCampusName] [nvarchar](128) NULL,
	[BizCustomerID] [nvarchar](36) NOT NULL,
	[BizCustomerCode] [nvarchar](64) NULL,
	[BizCustomerName] [nvarchar](128) NULL,
	[BizAccountID] [nvarchar](36) NULL,
	[BizAccountCode] [nvarchar](64) NULL,
    [BizAccountType] NVARCHAR(32) NULL, 
    [BizThatDiscountID] NVARCHAR(36) NULL, 
    [BizThatDiscountCode] NVARCHAR(64) NULL, 
    [BizThatDiscountBase] DECIMAL(18, 4) NULL, 
    [BizThatDiscountRate] DECIMAL(18, 2) NULL, 
    [BizThatAccountValue] DECIMAL(18, 4) NULL, 
    [BizThatAccountMoney] DECIMAL(18, 4) NULL, 
    [BizThisDiscountID] NVARCHAR(36) NULL, 
    [BizThisDiscountCode] NVARCHAR(64) NULL, 
    [BizThisDiscountBase] DECIMAL(18, 4) NULL, 
    [BizThisDiscountRate] DECIMAL(18, 2) NULL, 
    [BizThisAccountValue] DECIMAL(18, 4) NULL, 
    [BizThisAccountMoney] DECIMAL(18, 4) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountTransferApplies] PRIMARY KEY NONCLUSTERED 
(
	[ApplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AccountTransferApplies] UNIQUE NONCLUSTERED 
(
	[ApplyNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountTransferApplies_1]    Script Date: 2016/3/23 15:30:54 ******/


GO
/****** Object:  Index [IX_AccountTransferApplies_2]    Script Date: 2016/3/23 15:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_AccountTransferApplies_2] ON [CM].[AccountTransferApplies]
([CustomerID], [ApplyTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO
/****** Object:  Index [IX_AccountTransferApplies_3]    Script Date: 2016/3/23 15:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_AccountTransferApplies_3] ON [CM].[AccountTransferApplies]
([AccountID], [ApplyTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountTransferApplies_4]    Script Date: 2016/3/23 15:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_AccountTransferApplies_4] ON [CM].[AccountTransferApplies]
(
	[ApplyTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'AccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单号' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplyNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请状态（参考缴费单）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplyStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转让原因' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplyMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplierJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ApplierJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'转让金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'TransferMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'SubmitterID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'SubmitterName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'SubmitterJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'SubmitterJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'SubmitTime'
GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name='ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountTransferApplies', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizCustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizAccountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户转让申请表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'AccountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproveTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理状态（参考订购）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理说明',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让前折扣ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让前折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让前折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让前账户价值',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatAccountValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让后账户价值',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisAccountValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让后折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让后折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让后折扣ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizCampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizCampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizCustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizCustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizAccountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让前折扣编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让后折扣编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让前账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatAccountMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让后账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisAccountMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让前的折扣ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThatDiscountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让前的折扣编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThatDiscountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让前的折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThatDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让前的折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThatDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让前的账户价值',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThatAccountValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让前的账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThatAccountMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让后的折扣ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThisDiscountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让后的折扣编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThisDiscountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让后的折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThisDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让后的折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThisDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让后的账户价值',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThisAccountValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至账户转让后的账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'BizThisAccountMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转让类型（1-转出，2-转入）只有转出，在视图里体现转入',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'TransferType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'AccountType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'对方账户类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'BizAccountType'