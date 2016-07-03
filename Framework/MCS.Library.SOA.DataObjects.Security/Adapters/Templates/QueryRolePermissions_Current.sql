SELECT SC.*
FROM SC.SchemaApplicationSnapshot A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot M (NOLOCK) ON A.ID = M.ContainerID
	INNER JOIN SC.SchemaRoleSnapshot P (NOLOCK) ON M.MemberID = P.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot R (NOLOCK) ON R.ParentID = P.ID
	INNER JOIN SC.SchemaObject SC (NOLOCK) ON R.ObjectID = SC.ID
WHERE {0}
ORDER BY M.InnerSort