﻿/* SchemaMemberSnapshot 的当前视图，和索引*/

CREATE VIEW [SC].[SchemaMembersSnapshot_Current]
WITH SCHEMABINDING 
AS
SELECT [ContainerID], [MemberID], [VersionStartTime], [VersionEndTime], [Status], [CreateDate], [InnerSort], [ContainerSchemaType], [MemberSchemaType], [SearchContent], [RowUniqueID], [SchemaType], [CreatorID], [CreatorName]
FROM [SC].[SchemaMembersSnapshot]
WHERE [VersionEndTime] = CONVERT(DATETIME, '99990909 00:00:00', 112) AND [Status] = 1

GO

/****** Object:  Index [SchemaMembersSnapshot_Current_ClusteredIndex]    Script Date: 2013/5/17 16:16:13 ******/
CREATE UNIQUE CLUSTERED INDEX [SchemaMembersSnapshot_Current_ClusteredIndex] ON [SC].[SchemaMembersSnapshot_Current]
(
	[ContainerID] ASC,
	[MemberID] ASC,
	[VersionStartTime] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF)
GO

CREATE UNIQUE INDEX [IX_SchemaMembersSnapshot_Current_RowID] ON [SC].[SchemaMembersSnapshot_Current] ([RowUniqueID])

GO

CREATE INDEX [IX_SchemaMembersSnapshot_Current_MemberID] ON [SC].[SchemaMembersSnapshot_Current] ([MemberID])

GO
