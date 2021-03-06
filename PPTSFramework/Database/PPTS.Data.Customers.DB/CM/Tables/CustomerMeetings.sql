
CREATE TABLE [CM].[CustomerMeetings](
	[CampusID] [nvarchar](36) NOT NULL,
	[CampusName] [nvarchar](128) NULL,
	[CustomerID] [nvarchar](36) NOT NULL,
	[MeetingID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[MeetingTime] [datetime] NOT NULL,
    [MeetingEndTime] DATETIME NULL , 
	[MeetingDuration] NVARCHAR(64) NULL,
    [MeetingDurationValue] DECIMAL(18, 2) NULL, 
	[MeetingType] [nvarchar](32) NULL,
	[MeetingEvent] [nvarchar](32) NULL,
    [MeetingTitle] NVARCHAR(32) NULL, 
	[Satisficing] [nvarchar](32) NULL,
	[OrganizerID] [nvarchar](36) NULL,
	[OrganizerName] [nvarchar](64) NULL,
	[OrganizerJobID] [nvarchar](36) NULL,
	[OrganizerJobName] [nvarchar](64) NULL,
	[NextMeetingTime] [datetime] NULL,
	[Participants] [nvarchar](MAX) NULL,
	[CreatorID] [nvarchar](36) NULL,
	[CreatorName] [nvarchar](64) NULL,
	[CreateTime] [datetime] NULL DEFAULT GETUTCDATE(),
	[ModifierID] [nvarchar](36) NULL,
	[ModifierName] [nvarchar](64) NULL,
	[ModifyTime] [datetime] NULL DEFAULT GETUTCDATE(),
 [TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerMeetings] PRIMARY KEY NONCLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'校区ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'CampusID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学员ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学情会ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'MeetingID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开会时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'MeetingTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开会时长代码（兼容老数据）' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name='MeetingDuration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议类型代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name='MeetingType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会谈事件代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name='MeetingEvent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议组织者ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'OrganizerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议组织者姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'OrganizerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议组织者岗位ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'OrganizerJobID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议组织者岗位名称' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'OrganizerJobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计下次会议时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'NextMeetingTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满意度代码' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name='Satisficing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议参与人列表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'Participants'
GO

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'CreatorID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'ModifierID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人姓名' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'ModifierName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学情会信息表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetings'
GO

CREATE INDEX [IX_CustomerMeetings_2] ON [CM].[CustomerMeetings] ([CustomerID], [MeetingTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区名称',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerMeetings',
    @level2type = N'COLUMN',
    @level2name = N'CampusName'
GO

CREATE INDEX [IX_CustomerMeetings_3] ON [CM].[CustomerMeetings] ([MeetingTime])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'会议主题代码',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerMeetings',
    @level2type = N'COLUMN',
    @level2name = N'MeetingTitle'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'会议结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerMeetings',
    @level2type = N'COLUMN',
    @level2name = N'MeetingEndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'会议时长（分钟）',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerMeetings',
    @level2type = N'COLUMN',
    @level2name = N'MeetingDurationValue'