SELECT SC.*, SR.ParentID, IsNull(SR.InnerSort, 0) AS InnerSort, SR.FullPath, SR.GlobalSort, IsNull(SR.IsDefault, 1) AS IsDefault
FROM SC.UserAndContainerSnapshot UC (NOLOCK) INNER JOIN SC.SchemaObjectSnapshot SCContainer (NOLOCK) ON UC.ContainerID = SCContainer.ID
	INNER JOIN SC.SchemaRelationObjectsSnapshot SRContainer ON UC.ContainerID = SRContainer.ObjectID
		INNER JOIN SC.SchemaRelationObjectsSnapshot SR (NOLOCK) ON UC.UserID = SR.ObjectID
		INNER JOIN SC.SchemaObjectSnapshot SC (NOLOCK) ON SC.ID = SR.ObjectID
WHERE {0}