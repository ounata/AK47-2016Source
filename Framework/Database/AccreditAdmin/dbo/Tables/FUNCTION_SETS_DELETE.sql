﻿CREATE TABLE [dbo].[FUNCTION_SETS_DELETE] (
    [ID]             NVARCHAR (36) NOT NULL,
    [APP_ID]         NVARCHAR (36) NOT NULL,
    [NAME]           NVARCHAR (32) NOT NULL,
    [CODE_NAME]      NVARCHAR (32) NOT NULL,
    [DESCRIPTION]    NVARCHAR (32) NULL,
    [SORT_ID]        INT           NULL,
    [CHILDREN_COUNT] INT           NOT NULL,
    [RESOURCE_LEVEL] NVARCHAR (32) NOT NULL,
    [LOWEST_SET]     NCHAR (1)     NULL,
    [INHERITED]      NCHAR (1)     NULL,
    [CLASSIFY]       NCHAR (1)     NULL,
    [MODIFY_TIME]    DATETIME      NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_FUNCTION_SETS_DELETE]
    ON [dbo].[FUNCTION_SETS_DELETE]([APP_ID] ASC, [ID] ASC) WITH (FILLFACTOR = 50, PAD_INDEX = ON);
