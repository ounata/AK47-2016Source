SELECT SC.*, A.ID AS AppID
FROM SC.SchemaApplicationSnapshot A (NOLOCK)
	INNER JOIN SC.SchemaMembersSnapshot MA (NOLOCK) ON A.ID = MA.ContainerID
	INNER JOIN SC.SchemaObject SC (NOLOCK) ON MA.MemberID = SC.ID
	INNER JOIN SC.UserAndContainerSnapshot UC (NOLOCK) ON UC.ContainerID = SC.ID
WHERE {0}
ORDER BY MA.InnerSort
