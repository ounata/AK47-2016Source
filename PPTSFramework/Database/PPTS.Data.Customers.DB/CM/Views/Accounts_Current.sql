CREATE VIEW [CM].[Accounts_Current]
WITH SCHEMABINDING 
AS
SELECT [CustomerID]
      ,[AccountID]
      ,[AccountCode]
      ,[AccountType]
      ,[AccountMemo]
      ,[AccountStatus]
      ,[AccountMoney]
      ,[DiscountID]
      ,[DiscountCode]
      ,[DiscountRate]
      ,[DiscountBase]
      ,[ChargeApplyID]
      ,[ChargePayTime]
      ,[FirstChargePayTime]
      ,[FirstChargeApplyID]
      ,[CreatorID]
      ,[CreatorName]
      ,[CreateTime]
      ,[ModifierID]
      ,[ModifierName]
      ,[ModifyTime]
	  ,[VersionStartTime]
	  ,[VersionEndTime]
  FROM [CM].[Accounts]
  WHERE   (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))

GO

CREATE UNIQUE CLUSTERED INDEX [PK_Accounts_Current] ON [CM].[Accounts_Current]
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

CREATE UNIQUE INDEX [IX_Accounts_Current] ON [CM].[Accounts_Current]
(
	[AccountCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

CREATE NONCLUSTERED INDEX [IX_Accounts_Current_1] ON [CM].[Accounts_Current]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO