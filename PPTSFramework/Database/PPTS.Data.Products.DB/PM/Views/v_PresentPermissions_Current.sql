CREATE VIEW [PM].[v_PresentPermissions_Current]
	AS SELECT * FROM [PM].[PresentPermissions]  where getutcdate()>=StartDate and EndDate<getutcdate()
