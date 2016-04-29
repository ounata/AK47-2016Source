CREATE VIEW [CM].[ParentsFulltext]
WITH SCHEMABINDING
AS
SELECT OwnerID, OwnerType, CustomerSearchContent, ParentSearchContent, CreatorID, CreatorName, CreateTime, TenantCode
FROM [CM].[CustomerFulltextInfo]
WHERE OwnerType = 'Parents'

GO

CREATE UNIQUE CLUSTERED INDEX [ParentsFulltext_ClusteredIndex] ON [CM].[ParentsFulltext]
(
	[OwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE FULLTEXT INDEX ON [CM].[ParentsFulltext]
    (
	[CustomerSearchContent] LANGUAGE 2052,
	[ParentSearchContent] LANGUAGE 2052
	)
    KEY INDEX [ParentsFulltext_ClusteredIndex]
    ON [PPTSCustomerCatalog] WITH CHANGE_TRACKING AUTO
GO