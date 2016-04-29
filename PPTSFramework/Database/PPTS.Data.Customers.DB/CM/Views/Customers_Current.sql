
CREATE VIEW [CM].[Customers_Current]
WITH SCHEMABINDING 
AS
 select [CampusID]
      ,[CampusName]
      ,[CustomerID]
      ,[CustomerCode]
      ,[CustomerName]
      ,[CustomerLevel]
      ,[CustomerStatus]
      ,[StudentStatus]
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
      ,[Locked]
      ,[LockMemo]
      ,[Graduated]
      ,[GraduateYear]
      ,[SchoolID]
      ,[IsStudyAgain]
      ,[FirstSignTime]
      ,[FirstSignerID]
      ,[FirstSignerName]
      ,[FollowTime]
      ,[FollowStage]
      ,[FollowedCount]
      ,[NextFollowTime]
      ,[VistTime]
      ,[VisitedCount]
      ,[NextVistTime]
      ,[CreatorID]
      ,[CreatorName]
      ,[CreateTime]
      ,[ModifierID]
      ,[ModifierName]
      ,[ModifyTime]
      ,[TenantCode]
      ,[VersionStartTime]
      ,[VersionEndTime]
  FROM [CM].[Customers]
  WHERE   (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [CustomersCurrent_ClusteredIndex] ON [CM].[Customers_Current]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

CREATE UNIQUE INDEX [CustomersCustomerCodeIndex] ON [CM].[Customers_Current]
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO