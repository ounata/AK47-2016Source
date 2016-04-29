CREATE VIEW [CM].[Phones_Current]
WITH SCHEMABINDING 
AS
SELECT   OwnerID, ItemID, IsPrimary, PhoneType, CountryCode, AreaNumber, PhoneNumber, Extension, CreatorID, CreatorName, 
                CreateTime, TenantCode, VersionStartTime, VersionEndTime
FROM      CM.Phones
WHERE  (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [Phones_Current_ClusteredIndex] ON [CM].[Phones_Current]
(
	[OwnerID] ASC,
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO