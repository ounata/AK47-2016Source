﻿CREATE TABLE [dbo].[ORGANIZATIONS_AD] (
    [GUID]             NVARCHAR (36)  NOT NULL,
    [DISPLAY_NAME]     NVARCHAR (64)  NOT NULL,
    [OBJ_NAME]         NVARCHAR (64)  NOT NULL,
    [PARENT_GUID]      NVARCHAR (36)  NULL,
    [RANK_CODE]        NVARCHAR (32)  NOT NULL,
    [INNER_SORT]       NVARCHAR (6)   NOT NULL,
    [ORIGINAL_SORT]    NVARCHAR (255) NOT NULL,
    [GLOBAL_SORT]      NVARCHAR (255) NOT NULL,
    [ALL_PATH_NAME]    NVARCHAR (255) NOT NULL,
    [ORG_CLASS]        INT            NOT NULL,
    [ORG_TYPE]         INT            NOT NULL,
    [CHILDREN_COUNTER] INT            NOT NULL,
    [STATUS]           INT            NOT NULL,
    [CUSTOMS_CODE]     NVARCHAR (4)   NULL,
    [DESCRIPTION]      NVARCHAR (255) NULL,
    [CREATE_TIME]      DATETIME       NOT NULL,
    [MODIFY_TIME]      DATETIME       NOT NULL,
    [SYSDISTINCT1]     NVARCHAR (16)  NULL,
    [SYSDISTINCT2]     NVARCHAR (32)  NULL,
    [SYSCONTENT1]      NVARCHAR (32)  NULL,
    [SYSCONTENT2]      NVARCHAR (64)  NULL,
    [SYSCONTENT3]      NVARCHAR (128) NULL,
    [SEARCH_NAME]      NVARCHAR (255) NULL
);

