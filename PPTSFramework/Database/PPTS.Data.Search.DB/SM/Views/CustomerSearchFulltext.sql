
CREATE FULLTEXT INDEX ON [SM].[CustomerSearch]
    (
		[SearchContent] LANGUAGE 2052
	)
    KEY INDEX [PK_CustomerSearch]
    ON [PPTSCustomerCatalog] WITH CHANGE_TRACKING AUTO
GO