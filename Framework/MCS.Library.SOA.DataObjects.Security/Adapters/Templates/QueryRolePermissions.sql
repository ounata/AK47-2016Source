SELECT SC.*
FROM SC.SchemaApplicationSnapshot A
	INNER JOIN SC.SchemaMembersSnapshot M ON A.ID = M.ContainerID
	INNER JOIN SC.SchemaRoleSnapshot P ON M.MemberID = P.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot R ON R.ParentID = P.ID
	INNER JOIN SC.SchemaObject SC ON R.ObjectID = SC.ID
WHERE {0}
ORDER BY M.InnerSort