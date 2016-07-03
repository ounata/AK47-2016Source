
CREATE VIEW [CM].[PotentialCustomers_Current]
WITH SCHEMABINDING 
AS
SELECT [CustomerID]
      ,[OrgID]
      ,[OrgName]
      ,[OrgType]
      ,[CampusID]
      ,[CampusName]
      ,[CustomerCode]
      ,[CustomerName]
      ,[CustomerLevel]
      ,[InvalidReason]
      ,[CustomerStatus]
      ,[IsOneParent]
      ,[Character]
      ,[Birthday]
      ,[Gender]
      ,[Email]
      ,[IDType]
      ,[IDNumber]
      ,[VipType]
      ,[VipLevel]
      ,[EntranceGrade]
      ,[Grade]
      ,[SchoolYear]
      ,[SubjectType]
      ,[StudentType]
      ,[ContactType]
      ,[SourceMainType]
      ,[SourceSubType]
      ,[SourceSystem]
      ,[ReferralStaffID]
      ,[ReferralStaffName]
      ,[ReferralStaffJobID]
      ,[ReferralStaffJobName]
      ,[ReferralStaffOACode]
      ,[ReferralCustomerID]
      ,[ReferralCustomerCode]
      ,[ReferralCustomerName]
      ,[PurchaseIntention]
      ,[SchoolID]
      ,[IsStudyAgain]
      ,[FollowTime]
      ,[FollowStage]
      ,[FollowedCount]
      ,[NextFollowTime]
      ,[CreatorID]
      ,[CreatorName]
	  ,[CreatorJobType]
      ,[CreateTime]
      ,[ModifierID]
      ,[ModifierName]
      ,[ModifyTime]
      ,[TenantCode]
      ,[VersionStartTime]
      ,[VersionEndTime]
FROM      CM.PotentialCustomers
WHERE   (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [PotentialCustomersCurrent_ClusteredIndex] ON [CM].[PotentialCustomers_Current]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

CREATE UNIQUE INDEX [PotentialCustomersCustomerCodeIndex] ON [CM].[PotentialCustomers_Current]
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO