SELECT SC.ID, SC.Name, SC.DisplayName, SC.CodeName, SR.ParentID, SR.InnerSort, SR.GlobalSort, SR.IsDefault, SC.Status, SC.SchemaType,SR.FullPath
FROM SC.SchemaObjectSnapshot_Current SCContainer (NOLOCK)
	INNER JOIN SC.SchemaRelationObjectsSnapshot_Current SRContainer (NOLOCK) ON SCContainer.ID = SRContainer.ObjectID,
	SC.SchemaRelationObjectsSnapshot_Current SR (NOLOCK)
	INNER JOIN SC.SchemaObjectSnapshot_Current SC (NOLOCK) ON SC.ID = SR.ObjectID
WHERE {0}
ORDER BY SR.GlobalSort