SELECT SC.*
FROM SC.SchemaApplicationSnapshot_Current A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot_Current M (NOLOCK) ON A.ID = M.ContainerID
	INNER JOIN SC.SchemaPermissionSnapshot_Current P (NOLOCK) ON M.MemberID = P.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot_Current R (NOLOCK) ON R.ObjectID = P.ID
	INNER JOIN SC.SchemaObject_Current SC (NOLOCK) ON R.ParentID = SC.ID
WHERE {0}
ORDER BY M.InnerSort