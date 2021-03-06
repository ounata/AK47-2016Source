﻿
CREATE VIEW [OM].[Assets_Current]
WITH SCHEMABINDING 
AS

SELECT [AssetID]
      ,[AssetCode]
      ,[AssetName]
      ,[AssetType]
      ,[AssetRefType]
      ,[AssetRefPID]
      ,[AssetRefID]
      ,[AccountID]
      ,[CustomerID]
      ,[CustomerCode]
      ,[CustomerName]
      ,[ProductID]
      ,[ProductCode]
      ,[ProductName]
      ,[ProductUnit]
      ,[ProductUnitName]
      ,[Grade]
      ,[GradeName]
      ,[Subject]
      ,[SubjectName]
      ,[Catalog]
      ,[CatalogName]
      ,[CategoryType]
      ,[CategoryTypeName]
      ,[CourseLevel]
      ,[CourseLevelName]
      ,[LessonDuration]
      ,[LessonDurationValue]
      ,[OrderPrice]
      ,[OrderAmount]
      ,[PresentAmount]
      ,[TunlandRate]
      ,[SpecialRate]
      ,[DiscountType]
      ,[DiscountRate]
      ,[RealPrice]
      ,[RealAmount]
      ,[ExpirationDate]
      ,[AssignedAmount]
      ,[ConfirmedAmount]
      ,[ExchangedAmount]
      ,[DebookedAmount]
      ,[ConfirmedMoney]
      ,[ReturnedMoney]
      ,[Amount]
      ,[Price]
      ,[CreatorID]
      ,[CreatorName]
      ,[CreateTime]
      ,[ModifierID]
      ,[ModifierName]
      ,[ModifyTime]
      ,[VersionStartTime]
      ,[VersionEndTime]
  FROM [OM].[Assets]
  WHERE (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [PK_Assets_Current] ON [OM].[Assets_Current]
(
	[AssetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

CREATE UNIQUE INDEX [IX_Assets_Current] ON [OM].[Assets_Current]
(
	[AssetCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE UNIQUE INDEX [IX_Assets_Current_1] ON [OM].[Assets_Current]
(
	[AssetRefID] ASC,
	[AssetRefType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


