CREATE TABLE [TX].[TRANSACTION_PROCESS]
(
	[PROCESS_ID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
	[CURRENT_ACTIVITY_INDEX] INT NULL DEFAULT -1,
    [PROCESS_NAME] NVARCHAR(64) NULL, 
    [CATEGORY] NVARCHAR(64) NULL, 
    [CONNECTION_NAME] NVARCHAR(255) NULL, 
    [STATUS] NVARCHAR(64) NULL, 
    [STATUS_TEXT] NVARCHAR(MAX) NULL, 
    [START_TIME] DATETIME NULL, 
    [END_TIME] DATETIME NULL, 
	[CREATOR_ID] NVARCHAR(36) NULL,
	[CREATOR_NAME] NVARCHAR(36) NULL,
    [CREATE_TIME] DATETIME NULL DEFAULT GETUTCDATE(), 
    [DATA] XML NULL
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程的ID',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'PROCESS_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程的名称',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'PROCESS_NAME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程的类别',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'CATEGORY'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程所属的连接名称',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'CONNECTION_NAME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程的状态',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'STATUS'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'状态内容',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'STATUS_TEXT'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'开始时间',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'START_TIME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'结束时间',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'END_TIME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建时间',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'CREATE_TIME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'流程的数据',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'DATA'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'事务流程表',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人ID',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'CREATOR_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建人名称',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'CREATOR_NAME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'当前活动的ID',
    @level0type = N'SCHEMA',
    @level0name = N'TX',
    @level1type = N'TABLE',
    @level1name = N'TRANSACTION_PROCESS',
    @level2type = N'COLUMN',
    @level2name = N'CURRENT_ACTIVITY_INDEX'