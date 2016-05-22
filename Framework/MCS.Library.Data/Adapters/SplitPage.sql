WITH TempQuery AS
(
    SELECT {0}{1}, ROW_NUMBER() OVER (ORDER BY {4}) AS 'RowNumberForSplit'
	FROM {2}
	WHERE 1 = 1 {3}
	{5}
)
SELECT * 
FROM TempQuery 
WHERE RowNumberForSplit BETWEEN {6} AND {7};