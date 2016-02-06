﻿CREATE VIEW [WF].[PROGRAM_AND_ALIAS_VIEW]
AS
SELECT APPLICATION_CODE_NAME, CODE_NAME, NAME, SORT
FROM WF.PROGRAMS P
WHERE P.APPLICATION_CODE_NAME NOT IN (SELECT APP_ALIAS_CODE_NAME FROM WF.APPLICATIONS_ALIAS)
UNION
SELECT AA.APP_CODE_NAME, P.CODE_NAME, P.NAME, SORT
FROM WF.PROGRAMS P INNER JOIN WF.APPLICATIONS_ALIAS AA ON P.APPLICATION_CODE_NAME = AA.APP_ALIAS_CODE_NAME