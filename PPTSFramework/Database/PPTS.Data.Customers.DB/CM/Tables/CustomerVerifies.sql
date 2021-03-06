
CREATE TABLE [CM].[CustomerVerifies](
	[CampusID] [nvarchar](36) NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[VerifyID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[VerifyTime] [datetime] NOT NULL DEFAULT GETUTCDATE(),
	[VerifierID] [nvarchar](36) NULL,
	[VerifierName] [nvarchar](64) NULL,
	[VerifierJobID] [nvarchar](36) NULL,
	[VerifierJobName] [nvarchar](64) NULL,
	[VerifyPeoples] [nvarchar](32) NULL,
	[VerifyRelations] [nvarchar](32) NULL,
    [PlanVerifyTime] DATETIME NULL, 
    [IsInvited] INT NULL DEFAULT 0, 
    [IsSigned] INT NULL DEFAULT 0, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerVerifies] PRIMARY KEY NONCLUSTERED 
(
	[VerifyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name='CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门确认ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'VerifyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门确认时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'VerifyTime'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门确认人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'VerifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门确认人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'VerifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门确认人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'VerifierJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门确认人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'VerifierJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际上门人数代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name='VerifyPeoples'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门人员关系代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name='VerifyRelations'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

GO

GO

GO


GO

CREATE INDEX [IX_CustomerVerifies_2] ON [CM].[CustomerVerifies] ([CustomerID], [VerifyTime])

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户上门确认表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVerifies',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVerifies',
    @level2type = N'COLUMN',
    @level2name = 'CampusName'
GO

GO

CREATE INDEX [IX_CustomerVerifies_3] ON [CM].[CustomerVerifies] ([VerifyTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否邀约（根据当时是否有跟进记录来判定）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVerifies',
    @level2type = N'COLUMN',
    @level2name = N'IsInvited'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否签约（缴费充值收款后插入一条记录表示签约）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVerifies',
    @level2type = N'COLUMN',
    @level2name = 'IsSigned'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预计上门时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVerifies',
    @level2type = N'COLUMN',
    @level2name = N'PlanVerifyTime'