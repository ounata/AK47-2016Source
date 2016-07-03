SELECT SC.*
FROM SC.SchemaApplicationSnapshot_Current A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot_Current M (NOLOCK) ON A.ID = M.ContainerID
	INNER JOIN SC.SchemaObject_Current SC (NOLOCK) ON M.MemberID = SC.ID
WHERE {0}
ORDER BY M.InnerSort
