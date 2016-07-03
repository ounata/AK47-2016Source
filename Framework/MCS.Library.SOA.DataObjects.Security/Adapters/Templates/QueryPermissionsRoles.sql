SELECT SC.*
FROM SC.SchemaApplicationSnapshot A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot M (NOLOCK) ON A.ID = M.ContainerID
	INNER JOIN SC.SchemaPermissionSnapshot P (NOLOCK) ON M.MemberID = P.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot R (NOLOCK) ON R.ObjectID = P.ID
	INNER JOIN SC.SchemaObject SC (NOLOCK) ON R.ParentID = SC.ID
WHERE {0}
ORDER BY M.InnerSort