
CREATE TABLE [PM].[Presents](
	[PresentID] [nvarchar](36) NOT NULL,
	[PresentCode] [nvarchar](64) NOT NULL,
	[PresentName] [nvarchar](128) NULL,
	[PresentStatus] [nvarchar](32) NOT NULL,
	[StartDate] [date] NOT NULL,
    [BranchID] NVARCHAR(36) NULL, 
	[BranchName] [nvarchar](64) NULL,
	[SubmitTime] [datetime] NULL,
	[SubmitterID] [nvarchar](36) NULL,
	[SubmitterName] [nvarchar](64) NULL,
	[SubmitterJobId] [nvarchar](36) NULL,
	[SubmitterJobName] [nvarchar](64) NULL,
    [ApproverID] NVARCHAR(36) NULL, 
    [ApproverName] NVARCHAR(64) NULL, 
    [ApproverJobID] NVARCHAR(36) NULL, 
    [ApproverJobName] NVARCHAR(64) NULL, 
    [ApproveTime] DATETIME NULL, 
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL,
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL,
	[TenantCode] [nvarchar](36) NULL,
 CONSTRAINT [PK_Presents] PRIMARY KEY NONCLUSTERED 
(
	[PresentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Presents] UNIQUE NONCLUSTERED 
(
	[PresentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_Presents_1]    Script Date: 2016/3/24 13:20:04 ******/
CREATE NONCLUSTERED INDEX [IX_Presents_1] ON [PM].[Presents]
(
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [PM].[Presents] ADD  CONSTRAINT [DF_Presents_PresentID]  DEFAULT newid() FOR [PresentID]
GO
ALTER TABLE [PM].[Presents] ADD  CONSTRAINT [DF_Presents_CreateTime]  DEFAULT GETUTCDATE() FOR [CreateTime]
GO
ALTER TABLE [PM].[Presents] ADD  CONSTRAINT [DF_Presents_ModifyTime]  DEFAULT GETUTCDATE() FOR [ModifyTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'PresentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠编码' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'PresentCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'买赠名称' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'PresentName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（审批中，已完成，已拒绝）' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'PresentStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效日期' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'StartDate'
GO

GO

GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'SubmitTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'SubmitterID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'SubmitterName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'SubmitterJobId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人岗位名称' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'SubmitterJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'PM', @level1type=N'TABLE',@level1name=N'Presents', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'买赠表',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = NULL,
    @level2name = NULL
	
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = N'COLUMN',
    @level2name = N'ApproverID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = N'COLUMN',
    @level2name = N'ApproverName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批人岗位名称',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = N'COLUMN',
    @level2name = N'ApproverJobName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'最后审批时间',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = N'COLUMN',
    @level2name = N'ApproveTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司名称',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = N'COLUMN',
    @level2name = 'BranchName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'分公司ID',
    @level0type = N'SCHEMA',
    @level0name = N'PM',
    @level1type = N'TABLE',
    @level1name = N'Presents',
    @level2type = N'COLUMN',
    @level2name = 'BranchID'