CREATE VIEW [CM].[CustomerStaffRelations_Current]
WITH SCHEMABINDING 
AS
SELECT [CustomerID]
      ,[StaffID]
      ,[StaffName]
      ,[StaffJobID]
      ,[StaffJobName]
      ,[StaffJobOrgID]
      ,[StaffJobOrgName]
      ,[RelationType]
      ,[CreatorID]
      ,[CreatorName]
      ,[CreateTime]
      ,[VersionStartTime]
      ,[VersionEndTime]
	  ,[TenantCode]
	  ,[RowUniqueID]
FROM  CM.CustomerStaffRelations
WHERE (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [CustomerStaffRelations_Current_ClusteredIndex] ON [CM].[CustomerStaffRelations_Current]
(
	[RowUniqueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

CREATE FULLTEXT INDEX ON [CM].[CustomerStaffRelations_Current]
    (
		[StaffName] LANGUAGE 2052
	)
    KEY INDEX [CustomerStaffRelations_Current_ClusteredIndex]
    ON [PPTSCustomerCatalog] WITH CHANGE_TRACKING AUTO
GO