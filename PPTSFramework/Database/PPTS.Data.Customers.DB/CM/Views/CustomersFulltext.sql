﻿CREATE VIEW [CM].[CustomersFulltext]
WITH SCHEMABINDING
AS
SELECT OwnerID, OwnerType, CustomerSearchContent, ParentSearchContent, CustomerStatus, CreatorID, CreatorName, CreateTime, TenantCode
FROM [CM].[CustomerFulltextInfo]
WHERE OwnerType = 'Customers'

GO

CREATE UNIQUE CLUSTERED INDEX [CustomersFulltext_ClusteredIndex] ON [CM].[CustomersFulltext]
(
	[OwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE FULLTEXT INDEX ON [CM].[CustomersFulltext]
    (
	[CustomerSearchContent] LANGUAGE 2052,
	[ParentSearchContent] LANGUAGE 2052
	)
    KEY INDEX [CustomersFulltext_ClusteredIndex]
    ON [PPTSCustomerCatalog] WITH CHANGE_TRACKING AUTO
GO