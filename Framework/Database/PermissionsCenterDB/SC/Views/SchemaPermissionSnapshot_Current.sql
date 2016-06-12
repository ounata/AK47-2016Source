﻿/*SchemaPermissionSnapshot 的当前视图，和索引*/

CREATE VIEW [SC].[SchemaPermissionSnapshot_Current]
WITH SCHEMABINDING 
AS
SELECT [ID], [VersionStartTime], [VersionEndTime], [Status], [CreateDate], [Name], [DisplayName], [CodeName], [SearchContent], [RowUniqueID], [SchemaType], [CreatorID], [CreatorName], [Comment]
FROM [SC].[SchemaPermissionSnapshot]
WHERE [VersionEndTime] = CONVERT(DATETIME, '99990909 00:00:00', 112) AND [Status] = 1

GO

/****** Object:  Index [SchemaPermissionSnapshot_Current_ClusteredIndex]    Script Date: 2013/5/17 16:16:13 ******/
CREATE UNIQUE CLUSTERED INDEX [SchemaPermissionSnapshot_Current_ClusteredIndex] ON [SC].[SchemaPermissionSnapshot_Current]
(
	[ID] ASC,
	[VersionStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO

CREATE UNIQUE INDEX [IX_SchemaPermissionSnapshot_Current_RowID] ON [SC].[SchemaPermissionSnapshot_Current] ([RowUniqueID])

GO

CREATE INDEX [IX_SchemaPermissionSnapshot_Current_CodeName] ON [SC].[SchemaPermissionSnapshot_Current] ([CodeName])

GO

CREATE INDEX [IX_SchemaPermissionSnapshot_Current_Name] ON [SC].[SchemaPermissionSnapshot_Current] ([Name])

GO

