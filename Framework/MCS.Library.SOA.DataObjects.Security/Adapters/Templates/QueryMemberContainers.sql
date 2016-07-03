SELECT SCContainer.*, SRContainer.ParentID, IsNull(SRContainer.InnerSort, 0) AS InnerSort, SRContainer.FullPath, SRContainer.GlobalSort, IsNull(SRContainer.IsDefault, 1) AS IsDefault
FROM SC.SchemaMembersSnapshot UC
	INNER JOIN SC.SchemaObjectSnapshot SCContainer (NOLOCK) ON UC.ContainerID = SCContainer.ID 
	INNER JOIN SC.SchemaRelationObjectsSnapshot SRContainer (NOLOCK) ON UC.ContainerID = SRContainer.ObjectID 
	INNER JOIN SC.SchemaRelationObjectsSnapshot SR (NOLOCK) ON UC.MemberID = SR.ObjectID INNER JOIN SC.SchemaObjectSnapshot SC ON SC.ID = SR.ObjectID
WHERE {0}