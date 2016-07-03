SELECT SC.ID, SC.Name, SC.DisplayName, SC.CodeName, SR.ParentID, SR.InnerSort, SR.GlobalSort, SR.IsDefault, SC.Status, SC.SchemaType,SR.FullPath
FROM SC.SchemaObjectSnapshot SCContainer (NOLOCK)
	INNER JOIN SC.SchemaRelationObjectsSnapshot SRContainer (NOLOCK) ON SCContainer.ID = SRContainer.ObjectID,
	SC.SchemaRelationObjectsSnapshot SR (NOLOCK)
	INNER JOIN SC.SchemaObjectSnapshot SC (NOLOCK) ON SC.ID = SR.ObjectID
WHERE {0}
ORDER BY SR.GlobalSort