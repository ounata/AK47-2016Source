SELECT SC.*, SR.ParentID, IsNull(SR.InnerSort, 0) AS InnerSort, SR.FullPath, SR.GlobalSort, IsNull(SR.IsDefault, 1) AS IsDefault
FROM SC.UserAndContainerSnapshot_Current UC (NOLOCK)
	INNER JOIN SC.SchemaObjectSnapshot_Current SCContainer (NOLOCK) ON UC.ContainerID = SCContainer.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot_Current SRContainer (NOLOCK) ON UC.ContainerID = SRContainer.ObjectID
	INNER JOIN SC.SchemaRelationObjectsSnapshot_Current SR (NOLOCK) ON UC.UserID = SR.ObjectID
	INNER JOIN SC.SchemaObjectSnapshot_Current SC (NOLOCK) ON SC.ID = SR.ObjectID
WHERE {0}