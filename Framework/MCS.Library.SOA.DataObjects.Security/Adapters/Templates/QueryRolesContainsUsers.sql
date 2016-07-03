SELECT SC.*, SR.ParentID, IsNull(SR.InnerSort, 0) AS InnerSort, SR.FullPath, SR.GlobalSort, IsNull(SR.IsDefault, 1) AS IsDefault
FROM SC.SchemaApplicationSnapshot A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot MA (NOLOCK) ON A.ID = MA.ContainerID
	INNER JOIN SC.SchemaRoleSnapshot R (NOLOCK) ON MA.MemberID = R.ID
	INNER JOIN SC.UserAndContainerSnapshot UC (NOLOCK) ON UC.ContainerID = R.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot SR (NOLOCK) ON UC.UserID = SR.ObjectID
	INNER JOIN SC.SchemaObjectSnapshot SC (NOLOCK) ON SC.ID = SR.ObjectID
WHERE {0}
ORDER BY SR.GlobalSort
