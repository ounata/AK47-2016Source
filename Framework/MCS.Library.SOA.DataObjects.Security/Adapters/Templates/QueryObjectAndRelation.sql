SELECT {0} SC.ID, SC.Name, SC.DisplayName, SC.CodeName, SR.ParentID, SR.InnerSort, SR.GlobalSort, SR.IsDefault, SC.Status, SC.SchemaType,SR.FullPath
FROM SC.SchemaObjectSnapshot SC (NOLOCK) INNER JOIN SC.SchemaRelationObjectsSnapshot SR (NOLOCK) ON SC.ID = SR.ObjectID
WHERE {1} ORDER BY SR.GlobalSort