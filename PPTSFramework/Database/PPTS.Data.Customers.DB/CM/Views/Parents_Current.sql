CREATE VIEW [CM].[Parents_Current]
WITH SCHEMABINDING 
AS
SELECT   ParentID, ParentCode, ParentName, Gender, Email, Industry, Career, Income, Birthday, IDType, IDNumber, Country, 
                Province, City, County, StreetName, AddressDetail, CreatorID, CreatorName, CreateTime, ModifierID, ModifierName, 
                ModifyTime, TenantCode, VersionStartTime, VersionEndTime
FROM      CM.Parents
WHERE  (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [Parents_Current_ClusteredIndex] ON [CM].[Parents_Current]
(
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

CREATE UNIQUE INDEX [Parents_Current_ParentCodeIndex] ON [CM].[Parents_Current]
(
	[ParentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO