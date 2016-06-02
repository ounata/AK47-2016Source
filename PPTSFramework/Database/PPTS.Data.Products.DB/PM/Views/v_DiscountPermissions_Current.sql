CREATE VIEW [PM].[v_DiscountPermissions_Current]
	AS SELECT * FROM [PM].[DiscountPermissions]  where getutcdate()>=StartDate and getutcdate()<EndDate