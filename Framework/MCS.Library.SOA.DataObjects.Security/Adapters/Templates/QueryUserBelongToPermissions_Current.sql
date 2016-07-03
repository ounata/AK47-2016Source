SELECT SC.*, A.ID AS AppID
FROM SC.SchemaApplicationSnapshot A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot_Current MA (NOLOCK) ON A.ID = MA.ContainerID
	INNER JOIN SC.SchemaRoleSnapshot_Current R (NOLOCK) ON MA.MemberID = R.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot_Current SR (NOLOCK) ON SR.ParentID = R.ID
	INNER JOIN SC.SchemaObject_Current SC (NOLOCK) ON SR.ObjectID = SC.ID
	INNER JOIN SC.UserAndContainerSnapshot_Current UC (NOLOCK) ON UC.ContainerID = R.ID
WHERE {0}
