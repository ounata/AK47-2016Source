CREATE VIEW [dbo].[VersionedOrderItem_Current]
WITH SCHEMABINDING 
AS
SELECT OrderID, ItemID, ItemName, VersionStartTime, VersionEndTime
FROM dbo.VersionedOrderItem
WHERE (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))
