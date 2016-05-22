CREATE TABLE [WF].[ProcessQueues]
(
	[SID] BIGINT NOT NULL IDENTITY , 
    [ObjID] NVARCHAR(36) NULL, 
    [ObjType] NVARCHAR(32) NULL, 
    [ObjMemo] NVARCHAR(50) NULL, 
    [ObjPriority] INT NULL DEFAULT 99, 
    [ErrorMessage] NVARCHAR(255) NULL, 
    [QueuedTime] DATETIME NULL DEFAULT GETUTCDATE(), 
    CONSTRAINT [PK_ProcessQueues] PRIMARY KEY ([SID])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'入队序号',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = N'COLUMN',
    @level2name = N'SID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的对象ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = N'COLUMN',
    @level2name = N'ObjID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的对象类型',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = N'COLUMN',
    @level2name = N'ObjType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的对象说明',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = N'COLUMN',
    @level2name = N'ObjMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'入队列时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = N'COLUMN',
    @level2name = 'QueuedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的优先级',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = N'COLUMN',
    @level2name = 'ObjPriority'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理出错消息',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = N'COLUMN',
    @level2name = 'ErrorMessage'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理队列表',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueues',
    @level2type = NULL,
    @level2name = NULL