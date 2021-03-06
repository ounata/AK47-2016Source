

CREATE TABLE [CM].[CustomerVisits](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[VisitID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[VisitType] [nvarchar](32) NULL,
	[VisitWay] [nvarchar](32) NULL,
	[VisitContent] [nvarchar](MAX) NULL,
	[VisitTime] [datetime] NOT NULL,
	[VisitorID] [nvarchar](36) NULL,
	[VisitorName] [nvarchar](64) NULL,
	[VisitorJobID] [nvarchar](36) NULL,
	[VisitorJobName] [nvarchar](64) NULL,
	[NextVisitTime] [datetime] NULL,
    [Satisficing] NVARCHAR(32) NULL, 
    [RemindTime] DATETIME NULL , 
	[CreatorID] [nvarchar](32) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerVisits] PRIMARY KEY NONCLUSTERED 
(
	[VisitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name='CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'VisitID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'VisitTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'VisitorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'VisitorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访人岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'VisitorJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访人岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'VisitorJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访类型代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name='VisitType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访方式代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name='VisitWay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回访内容' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'VisitContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计下次回访时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'NextVisitTime'
GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerVisits', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

CREATE INDEX [IX_CustomerVisits_2] ON [CM].[CustomerVisits] ([CustomerID], [VisitTime])

GO

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'客户回访信息表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVisits',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVisits',
    @level2type = N'COLUMN',
    @level2name = 'CampusName'
GO


CREATE INDEX [IX_CustomerVisits_3] ON [CM].[CustomerVisits] ([VisitTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'满意度代码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVisits',
    @level2type = N'COLUMN',
    @level2name = N'Satisficing'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'提醒时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerVisits',
    @level2type = N'COLUMN',
    @level2name = N'RemindTime'