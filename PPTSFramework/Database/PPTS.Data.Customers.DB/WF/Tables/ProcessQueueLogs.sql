CREATE TABLE [WF].[ProcessQueueLogs]
(
	[SID] BIGINT NOT NULL, 
    [ObjID] NVARCHAR(36) NULL, 
    [ObjType] NVARCHAR(32) NULL, 
    [ObjMemo] NVARCHAR(50) NULL, 
    [ObjPriority] INT NULL, 
    [QueuedTime] DATETIME NULL, 
    [StartTime] DATETIME NULL, 
    [EndTime] DATETIME NULL 
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'入队序号',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = 'SID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的对象ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = N'ObjID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的对象类型',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = N'ObjType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的对象说明',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = N'ObjMemo'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的优先级',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = N'ObjPriority'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'入队列时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = N'QueuedTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = N'StartTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理的结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = N'COLUMN',
    @level2name = N'EndTime'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'处理队列日志表',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'ProcessQueueLogs',
    @level2type = NULL,
    @level2name = NULL