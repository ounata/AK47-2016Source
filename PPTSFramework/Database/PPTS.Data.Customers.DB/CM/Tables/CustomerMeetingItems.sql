
CREATE TABLE [CM].[CustomerMeetingItems](
	[MeetingID] [nvarchar](36) NOT NULL,
	[ItemID] [nvarchar](36) NOT NULL DEFAULT newid(),
	[ObjectType] [nvarchar](32) NOT NULL,
    [ObjectName] NVARCHAR(64) NULL, 
	[ContentType] [nvarchar](32) NOT NULL,
	[ContentData] [nvarchar](MAX) NULL,
	[TenantCode] NVARCHAR(36) NULL, 
    CONSTRAINT [PK_CustomerMeetingItems] PRIMARY KEY NONCLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


GO
/****** Object:  Index [IX_CustomerMeetingItems]    Script Date: 2016/3/23 15:15:17 ******/
CREATE NONCLUSTERED INDEX [IX_CustomerMeetingItems_1] ON [CM].[CustomerMeetingItems]
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学情会ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetingItems', @level2type=N'COLUMN',@level2name=N'MeetingID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学情会详情ID' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetingItems', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参与对象类型' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetingItems', @level2type=N'COLUMN',@level2name='ObjectType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议内容类型' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetingItems', @level2type=N'COLUMN',@level2name='ContentType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会议内容数据' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetingItems', @level2type=N'COLUMN',@level2name=N'ContentData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学情会明细表' , @level0type=N'SCHEMA',@level0name=N'CM', @level1type=N'TABLE',@level1name=N'CustomerMeetingItems'
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'参会对象姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerMeetingItems',
    @level2type = N'COLUMN',
    @level2name = N'ObjectName'