﻿CREATE TABLE [WF].[POST_USERS] (
    [POST_ID]   NVARCHAR (36) NOT NULL,
    [USER_ID]   NVARCHAR (36) NOT NULL,
    [USER_NAME] NVARCHAR (64) NOT NULL,
    CONSTRAINT [PK_WF_POST_USERS] PRIMARY KEY CLUSTERED ([USER_ID] ASC, [POST_ID] ASC)
);
