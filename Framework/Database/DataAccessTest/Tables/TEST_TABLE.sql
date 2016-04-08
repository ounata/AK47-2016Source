﻿CREATE TABLE [dbo].[TEST_TABLE]
(
	[ID] NVARCHAR(36) NOT NULL PRIMARY KEY, 
    [NAME] NVARCHAR(64) NULL, 
    [AMOUNT] NVARCHAR(64) NULL, 
    [LOCAL_TIME] DATETIME NULL DEFAULT GETDATE(), 
    [UTC_TIME] DATETIME NULL DEFAULT GETUTCDATE()
)