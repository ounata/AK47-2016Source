
CREATE TABLE [CM].[AccountDeductApplies](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[CustomerCode] [nvarchar](64) NULL,
	[CustomerName] [nvarchar](128) NULL,
	[AccountID] [nvarchar](36) NOT NULL,
	[AccountCode] [nvarchar](64) NULL,
	[ApplyID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[ApplyNo] [nvarchar](32) NOT NULL,
	[ApplyStatus] [nvarchar](32) NOT NULL,
	[ApplyMemo] [nvarchar](MAX) NULL,
	[ApplyTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[ApplierID] [nvarchar](36) NULL,
	[ApplierName] [nvarchar](64) NULL,
	[ApplierJobID] [nvarchar](36) NULL,
	[ApplierJobName] [nvarchar](64) NULL,
	[ApplierJobType] [nvarchar](32) NULL,
    [ProcessStatus] NVARCHAR(32) NULL DEFAULT '0' , 
    [ProcessTime] DATETIME NULL, 
    [ProcessMemo] NVARCHAR(255) NULL, 
	[ExpenseID] [nvarchar](36) NOT NULL,
	[ExpenseType] [nvarchar](32) NOT NULL,
	[ExpenseMoney] [decimal](18, 4) NOT NULL DEFAULT 0,
    [ThatDiscountID] NVARCHAR(36) NULL, 
    [ThatDiscountCode] NVARCHAR(64) NULL, 
    [ThatDiscountBase] DECIMAL(18, 4) NULL, 
    [ThatDiscountRate] DECIMAL(18, 2) NULL, 
    [ThatAccountValue] DECIMAL(18, 4) NULL, 
    [ThatAccountMoney] DECIMAL(18, 4) NULL, 
    [ThisDiscountID] NVARCHAR(36) NULL, 
    [ThisDiscountCode] NVARCHAR(64) NULL, 
    [ThisDiscountBase] DECIMAL(18, 4) NULL, 
    [ThisDiscountRate] DECIMAL(18, 2) NULL, 
    [ThisAccountValue] DECIMAL(18, 4) NULL, 
    [ThisAccountMoney] DECIMAL(18, 4) NULL, 
	[ConsultantID] [nvarchar](36) NULL,
	[ConsultantName] [nvarchar](64) NULL,
	[ConsultantJobID] [nvarchar](36) NULL,
	[EducatorID] [nvarchar](36) NULL,
	[EducatorName] [nvarchar](64) NULL,
	[EducatorJobID] [nvarchar](36) NULL,
	[SubmitterID] [nvarchar](36) NULL,
	[SubmitterName] [nvarchar](64) NULL,
	[SubmitterJobID] [nvarchar](36) NULL,
	[SubmitterJobName] [nvarchar](64) NULL,
	[SubmitterJobType] [nvarchar](32) NULL,
	[SubmitTime] [datetime] NULL,
    [ApproverID] NVARCHAR(36) NULL, 
    [ApproverName] NVARCHAR(64) NULL, 
    [ApproverJobID] NVARCHAR(36) NULL, 
    [ApproverJobName] NVARCHAR(64) NULL, 
    [ApproveTime] DATETIME NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountDeductApplies] PRIMARY KEY NONCLUSTERED 
(
	[ApplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AccountDeductApplies] UNIQUE NONCLUSTERED 
(
	[ApplyNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountDeductApplies_1]    Script Date: 2016/3/23 15:26:50 ******/


GO
/****** Object:  Index [IX_AccountDeductApplies_2]    Script Date: 2016/3/23 15:26:50 ******/
CREATE NONCLUSTERED INDEX [IX_AccountDeductApplies_2] ON [CM].[AccountDeductApplies]
([CustomerID], [ApplyTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO
/****** Object:  Index [IX_AccountDeductApplies_3]    Script Date: 2016/3/23 15:26:50 ******/
CREATE NONCLUSTERED INDEX [IX_AccountDeductApplies_3] ON [CM].[AccountDeductApplies]
([AccountID], [ApplyTime])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountDeductApplies_4]    Script Date: 2016/3/23 15:26:50 ******/
CREATE NONCLUSTERED INDEX [IX_AccountDeductApplies_4] ON [CM].[AccountDeductApplies]
(
	[ApplyTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'AccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单号' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplyNo'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请状态（参考缴费单）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplyStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplyMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplierJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ApplierJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务费ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ExpenseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务费类型（参考服务费表）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ExpenseType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务费金额' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ExpenseMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ConsultantID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ConsultantName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询师岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ConsultantJobID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'EducatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'EducatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学管师岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'EducatorJobID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'SubmitterID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'SubmitterName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'SubmitterJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'SubmitterJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'SubmitTime'
GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountDeductApplies', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'服务费扣减申请表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'AccountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproveTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理状态（参考订购）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理说明',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减前折扣ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减前折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减前折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减前账户价值',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatAccountValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减后折扣ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减后折扣基数',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountBase'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减后折扣率',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountRate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减后账户价值',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisAccountValue'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交人岗位类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'SubmitterJobType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'申请人岗位类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApplierJobType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减前折扣编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatDiscountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减后折扣编码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisDiscountCode'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减前账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThatAccountMoney'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'扣减后账户余额',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountDeductApplies',
    @level2type = N'COLUMN',
    @level2name = N'ThisAccountMoney'