CREATE VIEW [CM].[CustomerParentRelations_Current]
WITH SCHEMABINDING 
AS
SELECT   CustomerID, CustomerRole, ParentID, ParentRole, IsPrimary, CreatorID, CreatorName, CreateTime, ModifierID, 
                ModifierName, ModifyTime, TenantCode, VersionStartTime, VersionEndTime
FROM      CM.CustomerParentRelations
WHERE  (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))
GO

CREATE UNIQUE CLUSTERED INDEX [CustomerParentRelations_Current_ClusteredIndex] ON [CM].[CustomerParentRelations_Current]
(
	[CustomerID] ASC,
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

CREATE INDEX [CustomerParentRelations_Current_ParentIDIndex] ON [CM].[CustomerParentRelations_Current]
(
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO