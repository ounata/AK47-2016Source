CREATE VIEW [dbo].[VersionedOrder_Current]
WITH SCHEMABINDING 
AS
SELECT OrderID, OrderName, Amount, VersionStartTime, VersionEndTime
FROM dbo.VersionedOrder
WHERE (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))
