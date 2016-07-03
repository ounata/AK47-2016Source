﻿CREATE TABLE [WF].[PENDING_ACTIVITIES_SERVICE] (
    [ACTIVITY_ID]       NVARCHAR (36) NOT NULL,
    [LAST_SERVICE_TIME] DATETIME      NULL,
	[TENANT_CODE] NVARCHAR(36) NULL DEFAULT 'D5561180-7617-4B67-B68B-1F0EA604B509'
    CONSTRAINT [PK_PENDING_ACTIVITIES_SERVICE] PRIMARY KEY CLUSTERED ([ACTIVITY_ID] ASC)
);

GO

CREATE INDEX [IX_PENDING_ACTIVITIES_SERVICE_TENANT_CODE] ON [WF].[PENDING_ACTIVITIES_SERVICE] ([TENANT_CODE])

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'不太明确用途',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'PENDING_ACTIVITIES_SERVICE',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'UTC时间',
    @level0type = N'SCHEMA',
    @level0name = N'WF',
    @level1type = N'TABLE',
    @level1name = N'PENDING_ACTIVITIES_SERVICE',
    @level2type = N'COLUMN',
    @level2name = N'LAST_SERVICE_TIME'