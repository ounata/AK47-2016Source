CREATE TABLE [WF].[FIXED_TIME_TASK]
(
	[TASK_GUID]		NVARCHAR(36) NOT NULL,
    [SORT_ID]		INT NOT NULL IDENTITY,
	[CATEGORY]		NVARCHAR (64)   NULL,
    [TASK_TYPE]		NVARCHAR (64)   NULL,
    [TASK_TITLE]	NVARCHAR (1024) NULL,
    [RESOURCE_ID]	NVARCHAR (36)   NULL,
    [CREATE_TIME]	DATETIME NULL DEFAULT GETUTCDATE(), 
    [START_TIME]	DATETIME NULL, 
	[SOURCE_ID]		NVARCHAR (36)   NULL,
    [SOURCE_NAME]	NVARCHAR (64)   NULL,
	[URL]			NVARCHAR (2048) NULL,
    [DATA]			XML  NULL, 
	[TENANT_CODE] NVARCHAR(36) NULL DEFAULT 'D5561180-7617-4B67-B68B-1F0EA604B509'
	CONSTRAINT [PK_FixedTimeTask] PRIMARY KEY NONCLUSTERED ([TASK_GUID] ASC)
)
GO
CREATE CLUSTERED INDEX [IX_FIXED_TIME_TASK_SortID] ON [WF].[FIXED_TIME_TASK] ([SORT_ID])

GO

CREATE INDEX [IX_FIXED_TIME_TASK_ResourceID] ON [WF].[FIXED_TIME_TASK] ([RESOURCE_ID])

GO

CREATE INDEX [IX_FIXED_TIME_TASK_SourceID] ON [WF].[FIXED_TIME_TASK] ([SOURCE_ID])

GO

CREATE FULLTEXT INDEX ON [WF].[FIXED_TIME_TASK] ([TASK_TITLE] LANGUAGE 2052)
KEY INDEX [PK_FixedTimeTask] ON [MCS_WORKFLOW] WITH CHANGE_TRACKING AUTO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'固定时间需要执行的任务',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务的PK，不是聚集索引',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'TASK_GUID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务的序号，自增且聚集索引',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'SORT_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务的分类',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'CATEGORY'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务的类别，会对应到不同的程序进行处理',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'TASK_TYPE'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务的标题',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'TASK_TITLE'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'关联ID，例如JOB_ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'RESOURCE_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务的创建时间，不是执行时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'CREATE_TIME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'预计的任务执行时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'START_TIME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者的ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'SOURCE_ID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'创建者的名称',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'SOURCE_NAME'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务相关的Url',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'URL'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'任务相关的数据',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'FIXED_TIME_TASK',
    @level2type = N'COLUMN',
    @level2name = N'DATA'
GO

CREATE INDEX [IX_FIXED_TIME_TASK_StartTime] ON [WF].[FIXED_TIME_TASK] ([START_TIME])
