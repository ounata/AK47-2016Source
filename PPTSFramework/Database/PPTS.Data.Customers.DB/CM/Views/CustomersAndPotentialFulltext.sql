CREATE VIEW [CM].[CustomersAndPotentialFulltext]
WITH SCHEMABINDING
AS
SELECT OwnerID, OwnerType, CustomerSearchContent, ParentSearchContent, CreatorID, CreatorName, CreateTime, TenantCode
FROM [CM].[CustomerFulltextInfo]
WHERE OwnerType IN 'Customers'