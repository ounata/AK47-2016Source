﻿CREATE VIEW [WF].[SYS_NOT_RUNNING_TASK] WITH SCHEMABINDING
AS
SELECT TASK_GUID, SORT_ID, CATEGORY, TASK_TYPE, TASK_TITLE, RESOURCE_ID, STATUS, CREATE_TIME, START_TIME, END_TIME, SOURCE_ID, 
                         SOURCE_NAME, URL, DATA, STATUS_TEXT, TENANT_CODE
FROM [WF].[SYS_TASK] WHERE [STATUS] = 'NotRunning'

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'未运行的系统任务' , @level0type=N'SCHEMA',@level0name=N'WF', @level1type=N'VIEW',@level1name=N'SYS_NOT_RUNNING_TASK'
GO