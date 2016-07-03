SELECT SC.*
FROM SC.SchemaApplicationSnapshot A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot M (NOLOCK) ON A.ID = M.ContainerID
	INNER JOIN SC.SchemaObject SC (NOLOCK) ON M.MemberID = SC.ID
WHERE {0}
ORDER BY M.InnerSort
