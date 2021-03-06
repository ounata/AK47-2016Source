

CREATE TABLE [CM].[CustomerTransferApplies](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[ApplyID] [nvarchar](36) NOT NULL DEFAULT newid(),
    [ApplyStatus] NVARCHAR(32) NOT NULL, 
	[ApplyTime] [datetime] NOT NULL,
	[ApplyMemo] [nvarchar](255) NULL,
	[ApplierID] [nvarchar](36) NULL,
	[ApplierName] [nvarchar](64) NULL,
	[ApplierJobID] [nvarchar](36) NULL,
	[ApplierJobName] [nvarchar](64) NULL,
    [ProcessStatus] NVARCHAR(32) NULL DEFAULT '0' , 
    [ProcessTime] DATETIME NULL, 
    [ProcessMemo] NVARCHAR(255) NULL, 
    [TransferType] NVARCHAR(32) NULL, 
    [ToCampusID] NVARCHAR(36) NOT NULL, 
	[ToCampusName] [nvarchar](128) NULL,
    [SubmitterID] NVARCHAR(36) NULL, 
    [SubmitterName] NCHAR(10) NULL, 
    [SubmitterJobID] NVARCHAR(36) NULL, 
    [SubmitterJobName] NVARCHAR(64) NULL, 
    [SubmitTime] DATETIME NOT NULL, 
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
    CONSTRAINT [PK_CustomerTransferApplies] PRIMARY KEY NONCLUSTERED 
(
	[ApplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name='CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name='ApplyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name='ApplyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name='ApplierID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name='ApplierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name='ApplierJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name='ApplierJobName'
GO
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApproveTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferApplies', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

CREATE INDEX [IX_CustomerTransferApplies_2] ON [CM].[CustomerTransferApplies] ([CustomerID], [ApplyTime])

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'SubmitterID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'SubmitterName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'SubmitterJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'SubmitterJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提交时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'SubmitTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'申请状态（审批中，已完成，已拒绝）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApplyStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ToCampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员转学申请表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'CampusName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ToCampusName'
GO

GO

GO

CREATE INDEX [IX_CustomerTransferApplies_3] ON [CM].[CustomerTransferApplies] ([ApplyTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转学类型（跨校区转学，跨分公司转学）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = 'TransferType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理状态（参考订购）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessStatus'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'异步处理说明',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ProcessMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转学原因',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferApplies',
    @level2type = N'COLUMN',
    @level2name = N'ApplyMemo'