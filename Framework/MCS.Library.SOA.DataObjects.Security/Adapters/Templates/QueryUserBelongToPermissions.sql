SELECT SC.*, A.ID AS AppID
FROM SC.SchemaApplicationSnapshot A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot MA (NOLOCK) ON A.ID = MA.ContainerID
	INNER JOIN SC.SchemaRoleSnapshot R (NOLOCK) ON MA.MemberID = R.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot SR (NOLOCK) ON SR.ParentID = R.ID
	INNER JOIN SC.SchemaObject SC (NOLOCK) ON SR.ObjectID = SC.ID
	INNER JOIN SC.UserAndContainerSnapshot UC (NOLOCK) ON UC.ContainerID = R.ID
WHERE {0}
