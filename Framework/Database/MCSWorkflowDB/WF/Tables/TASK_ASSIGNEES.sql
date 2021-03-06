﻿CREATE TABLE [WF].[TASK_ASSIGNEES] (
    [ID]            NVARCHAR (36)  NOT NULL,
    [RESOURCE_ID]   NVARCHAR (36)  NULL ,
    [INNER_ID]      INT            NULL,
    [TYPE]          NVARCHAR (36)  NULL,
    [ASSIGNEE_ID]   NVARCHAR (36)  NOT NULL,
    [ASSIGNEE_NAME] NVARCHAR (64)  NOT NULL,
    [URL]           NVARCHAR (MAX) NULL,
	[TENANT_CODE] NVARCHAR(36) NULL DEFAULT 'D5561180-7617-4B67-B68B-1F0EA604B509'
    CONSTRAINT [PK_TASK_ASSIGNEES_1] PRIMARY KEY CLUSTERED ([ID] ASC) 
);

GO

CREATE INDEX [IX_TASK_ASSIGNEES_TENANT_CODE] ON [WF].[TASK_ASSIGNEES] ([TENANT_CODE])

GO
CREATE NONCLUSTERED INDEX [IDX_TASK_ASSIGNEES_RESOURCE_ID]
    ON [WF].[TASK_ASSIGNEES]([RESOURCE_ID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务的分派人', @level0type = N'SCHEMA', @level0name = N'WF', @level1type = N'TABLE', @level1name = N'TASK_ASSIGNEES';


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'所属的对象的ID',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'TASK_ASSIGNEES',
    @level2type = N'COLUMN',
    @level2name = N'RESOURCE_ID'