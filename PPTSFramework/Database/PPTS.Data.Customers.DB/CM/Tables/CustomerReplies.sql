CREATE TABLE [CM].[CustomerReplies]
(
    [CampusID] NVARCHAR(36) NULL, 
    [CustomerID] NVARCHAR(36) NULL, 
	[ReplyID] NVARCHAR(36) NOT NULL DEFAULT newid(), 
    [ReplyTime] DATETIME NULL, 
    [ReplyType] NVARCHAR(32) NULL, 
    [ReplyContent] NVARCHAR(MAX) NULL, 
    [ReplyFrom] NVARCHAR(32) NULL, 
    [ReplierID] NVARCHAR(36) NULL, 
    [ReplierName] NVARCHAR(64) NULL, 
    [ParentID] NVARCHAR(36) NULL, 
    [ParentName] NVARCHAR(64) NULL, 
    [PhoneNumber] NVARCHAR(64) NULL, 
    [Poster] NVARCHAR(32) NULL, 
    [CreatorID] NVARCHAR(36) NULL, 
    [CreatorName] NVARCHAR(64) NULL, 
    [CreateTime] DATETIME NULL DEFAULT getdate(), 
    CONSTRAINT [PK_CustomerReplies] PRIMARY KEY NONCLUSTERED ([ReplyID])  
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'校区ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CampusID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学员ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CustomerID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Teacher;School;MngTeacher;Inquiry，Inquiry[咨询师反馈]--1 MngTeacher[学管师反馈]--2 Teacher[教师反馈]--3 School[校区反馈]--4 家长反馈信息--5 WMng[周反馈]--6',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CreatorName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'CreateTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ParentID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ParentName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'家长手机号',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'PhoneNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'发言人（Xueda,Customer)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = 'Poster'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复内容',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyContent'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复人ID',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplierID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复人姓名',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplierName'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'回复来源(iOS,Andriod,WebPPTS)',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = N'COLUMN',
    @level2name = N'ReplyFrom'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'学大对学员反馈回复表',
    @level0type = N'SCHEMA',
    @level0name = N'CM',
    @level1type = N'TABLE',
    @level1name = N'CustomerReplies',
    @level2type = NULL,
    @level2name = NULL
GO

CREATE INDEX [IX_CustomerReplies_2] ON [CM].[CustomerReplies] ([CustomerID], [ReplyTime])

GO

CREATE INDEX [IX_CustomerReplies_3] ON [CM].[CustomerReplies] ([ReplyTime])
