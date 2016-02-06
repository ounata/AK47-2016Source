﻿CREATE FULLTEXT INDEX ON [WF].[APPLICATIONS_COMMON_INFO]
		([SUBJECT] LANGUAGE 2052, [CONTENT] LANGUAGE 2052, [DRAFT_DEPARTMENT_NAME] LANGUAGE 2052)
		KEY INDEX [PK_APPLICATIONS_COMMON_INFO]
		ON [MCS_WORKFLOW];
GO

CREATE FULLTEXT INDEX ON [WF].[GENERIC_FORM_DATA]
	([SUBJECT] LANGUAGE 2052, [SEARCH_CONTENT] LANGUAGE 2052)
	KEY INDEX [PK_GENERIC_FORM_DATA]
	ON [MCS_WORKFLOW];
GO

CREATE FULLTEXT INDEX ON [WF].[USER_TASK]
	([TASK_TITLE] LANGUAGE 2052, [DRAFT_DEPARTMENT_NAME] LANGUAGE 1033, [DRAFT_USER_NAME] LANGUAGE 1033)
	KEY INDEX [PK_USER_TASK]
	ON [MCS_WORKFLOW];
GO

CREATE FULLTEXT INDEX ON [WF].[USER_ACCOMPLISHED_TASK]
	([TASK_TITLE] LANGUAGE 2052, [DRAFT_DEPARTMENT_NAME] LANGUAGE 1033, [DRAFT_USER_NAME] LANGUAGE 1033)
	KEY INDEX [PK_USER_ACCOMPLISHED_TASK]
	ON [MCS_WORKFLOW];
GO

CREATE FULLTEXT INDEX ON [WF].[GENERIC_FORM_RELATIVE_DATA]
	([SEARCH_CONTENT] LANGUAGE 2052)
	KEY INDEX [GENERIC_FORM_RELATIVE_DATA_ROW_ID]
	ON [MCS_WORKFLOW];
GO

CREATE FULLTEXT INDEX ON [WF].[MATERIAL_CONTENT]
	([CONTENT_DATA] TYPE COLUMN [FILE_NAME] LANGUAGE 2052)
	KEY INDEX [PK_MATERIAL_CONTENT]
	ON [MCS_WORKFLOW];