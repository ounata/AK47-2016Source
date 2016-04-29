CREATE VIEW [dbo].[VersionedOrder_Current]
WITH SCHEMABINDING 
AS
SELECT OrderID, OrderName, Amount, VersionStartTime, VersionEndTime
FROM dbo.VersionedOrder
WHERE (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [VersionedOrder_Current_ClusteredIndex] ON [dbo].[VersionedOrder_Current]
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO