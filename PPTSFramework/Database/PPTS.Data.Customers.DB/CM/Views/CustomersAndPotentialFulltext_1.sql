CREATE VIEW [CM].[CustomersAndPotentialFulltext]
WITH SCHEMABINDING
AS
SELECT OwnerID, OwnerType, CustomerSearchContent, ParentSearchContent, CustomerStatus, CreatorID, CreatorName, CreateTime, TenantCode
FROM [CM].[CustomerFulltextInfo]
WHERE (OwnerType = 'Customers' AND CustomerStatus = '10') OR (OwnerType = 'PotentialCustomers' AND CustomerStatus <> '10')

GO

CREATE UNIQUE CLUSTERED INDEX [CustomersAndPotentialFulltext_ClusteredIndex] ON [CM].[CustomersAndPotentialFulltext]
(
	[OwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE FULLTEXT INDEX ON [CM].[CustomersAndPotentialFulltext]
    (
	[CustomerSearchContent] LANGUAGE 2052,
	[ParentSearchContent] LANGUAGE 2052
	)
    KEY INDEX [CustomersAndPotentialFulltext_ClusteredIndex]
    ON [PPTSCustomerCatalog] WITH CHANGE_TRACKING AUTO
GO