

CREATE TABLE [CM].[AccountRefundVerifyings](
	[ApplyID] [nvarchar](36) NOT NULL,
	[VerifyID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[VerifyTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[VerifyAction] [nvarchar](32) NOT NULL,
	[VerifyMemo] [nvarchar](255) NULL,
	[VerifierID] [nvarchar](36) NULL,
	[VerifierName] [nvarchar](50) NULL,
	[VerifierJobID] [nvarchar](36) NULL,
	[VerifierJobName] [nvarchar](50) NULL,
    [VerifierOrgID] NVARCHAR(32) NULL, 
    [VerifierOrgName] NVARCHAR(64) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_AccountRefundVerifyings] PRIMARY KEY NONCLUSTERED 
(
	[VerifyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_AccountRefundVerifyings_1]    Script Date: 2016/3/23 15:30:22 ******/
CREATE NONCLUSTERED INDEX [IX_AccountRefundVerifyings_1] ON [CM].[AccountRefundVerifyings]
(
	[ApplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AccountRefundVerifyings_2]    Script Date: 2016/3/23 15:30:22 ******/
ALTER TABLE [CM].[AccountRefundVerifyings] ADD  CONSTRAINT [DF_AccountRefundVerifyings_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请单ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name=N'ApplyID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name='VerifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认操作（1-分出纳确认，2-分财务确认，3-分区域确认）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name='VerifyAction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认说明' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name='VerifyMemo'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name='VerifyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name='VerifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name='VerifierJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name='VerifierJobName'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'AccountRefundVerifyings', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

GO

GO

GO


GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'账户退费确认表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundVerifyings',
    @level2type = NULL,
    @level2name = NULL
GO

GO

GO

GO

GO

GO

GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认人组织机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundVerifyings',
    @level2type = N'COLUMN',
    @level2name = N'VerifierOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认人组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundVerifyings',
    @level2type = N'COLUMN',
    @level2name = N'VerifierOrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'确认人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'AccountRefundVerifyings',
    @level2type = N'COLUMN',
    @level2name = N'VerifierID'