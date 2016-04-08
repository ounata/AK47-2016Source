/*表类型，表示变化的数据对象*/
CREATE TYPE [SC].[ChangedObjects] AS TABLE
(
	ID NVARCHAR(36) NOT NULL,
	SchemaType NVARCHAR(64) NULL,
	VersionStartTime DATETIME NULL
	--PRIMARY KEY ([ID] ASC, [VersionStartTime] ASC)
)
