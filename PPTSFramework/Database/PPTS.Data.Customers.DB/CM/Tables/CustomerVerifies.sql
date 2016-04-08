
CREATE TABLE [CM].[CustomerVerifies](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[VerifyID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[VerifyTime] [datetime] NOT NULL,
	[VerifierID] [nvarchar](36) NULL,
	[VerifierName] [nvarchar](64) NULL,
	[VerifierJobID] [nvarchar](36) NULL,
	[VerifierJobName] [nvarchar](64) NULL,
	[VerifyNumber] [int] NOT NULL DEFAULT 1,
	[VerifyPeoples] [nvarchar](32) NULL,
	[VerifyRelations] [nvarchar](32) NULL,
	[IsPotential] INT NOT NULL DEFAULT 0,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NOT NULL DEFAULT getdate(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL DEFAULT getdate(),
	[ModifyTime] [datetime] NOT NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上门确认次第' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name='VerifyNumber'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVerifies', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'是否潜客阶段',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVerifies',
    @level2type = N'COLUMN',
    @level2name = 'IsPotential'
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
