SELECT SCContainer.*, SRContainer.ParentID, IsNull(SRContainer.InnerSort, 0) AS InnerSort, SRContainer.FullPath, SRContainer.GlobalSort, IsNull(SRContainer.IsDefault, 1) AS IsDefault
FROM SC.SchemaMembersSnapshot_Current UC (NOLOCK)
	INNER JOIN SC.SchemaObjectSnapshot_Current SCContainer (NOLOCK) ON UC.ContainerID = SCContainer.ID 
	INNER JOIN SC.SchemaRelationObjectsSnapshot_Current SRContainer (NOLOCK) ON UC.ContainerID = SRContainer.ObjectID 
	INNER JOIN SC.SchemaRelationObjectsSnapshot_Current SR (NOLOCK) ON UC.MemberID = SR.ObjectID INNER JOIN SC.SchemaObjectSnapshot_Current SC ON SC.ID = SR.ObjectID
WHERE {0}