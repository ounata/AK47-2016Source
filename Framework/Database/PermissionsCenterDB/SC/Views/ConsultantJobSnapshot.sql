﻿CREATE VIEW [SC].[ConsultantJobSnapshot_Current]
WITH SCHEMABINDING 
AS
SELECT [ID], [VersionStartTime], [VersionEndTime], [Status], [CreateDate], [Name], [DisplayName], [CodeName], [SearchContent], [RowUniqueID], [SchemaType], [CreatorID], [CreatorName], [Comment]
FROM [SC].[SchemaGroupSnapshot]
WHERE [VersionEndTime] = CONVERT(DATETIME, '99990909 00:00:00', 112) AND [Status] = 1 AND Name LIKE '%校教育咨询师'

GO

CREATE UNIQUE CLUSTERED INDEX [ConsultantJobSnapshot_Current_ClusteredIndex] ON [SC].[ConsultantJobSnapshot_Current]
(
	[ID] ASC,
	[VersionStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO