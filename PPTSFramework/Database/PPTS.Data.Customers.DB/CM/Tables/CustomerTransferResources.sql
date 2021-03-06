

CREATE TABLE [CM].[CustomerTransferResources](
	[OrgID] [nvarchar](36) NOT NULL,
	[OrgName] [nvarchar](128) NULL,
    [OrgType] NVARCHAR(32) NOT NULL, 
	[CustomerID] [nvarchar](36) NOT NULL,
	[TransferID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[TransferTime] [datetime] NOT NULL,
    [TransferMemo] NVARCHAR(MAX) NULL, 
	[TransferorID] [nvarchar](36) NULL,
	[TransferorName] [nvarchar](64) NULL,
	[TransferorJobID] [nvarchar](36) NULL,
	[TransferorJobName] [nvarchar](64) NULL,
    [ToOrgID] NVARCHAR(36) NOT NULL, 
	[ToOrgName] [nvarchar](128) NULL,
    [ToOrgType] NVARCHAR(32) NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerTransferResources] PRIMARY KEY NONCLUSTERED 
(
	[TransferID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name='OrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'划转ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name='TransferID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'划转时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name='TransferTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'划转人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name='TransferorID'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'划转人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name='TransferorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'划转人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name='TransferorJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'划转人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name='TransferorJobName'
GO

GO

GO

GO

GO

GO

GO

GO

GO

GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerTransferResources', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO

GO

GO

GO

CREATE INDEX [IX_CustomerTransferResources_2] ON [CM].[CustomerTransferResources] ([CustomerID], [TransferTime])

GO


GO

GO

GO

GO

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至组织机构ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferResources',
    @level2type = N'COLUMN',
    @level2name = 'ToOrgID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户划转资源表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferResources',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferResources',
    @level2type = N'COLUMN',
    @level2name = 'OrgName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至组织机构名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferResources',
    @level2type = N'COLUMN',
    @level2name = 'ToOrgName'
GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'组织机构类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferResources',
    @level2type = N'COLUMN',
    @level2name = N'OrgType'
GO

CREATE INDEX [IX_CustomerTransferResources_3] ON [CM].[CustomerTransferResources] ([TransferTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'划转原因',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferResources',
    @level2type = N'COLUMN',
    @level2name = N'TransferMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'转至组织机构类型',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerTransferResources',
    @level2type = N'COLUMN',
    @level2name = N'ToOrgType'