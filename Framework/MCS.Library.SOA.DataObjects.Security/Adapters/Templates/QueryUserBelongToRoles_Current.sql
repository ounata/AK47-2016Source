SELECT SC.*, A.ID AS AppID
FROM SC.SchemaApplicationSnapshot_Current A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot_Current MA (NOLOCK) ON A.ID = MA.ContainerID
	INNER JOIN SC.SchemaObject_Current SC (NOLOCK) ON MA.MemberID = SC.ID
	INNER JOIN SC.UserAndContainerSnapshot_Current UC (NOLOCK) ON UC.ContainerID = SC.ID
WHERE {0}
ORDER BY MA.InnerSort
