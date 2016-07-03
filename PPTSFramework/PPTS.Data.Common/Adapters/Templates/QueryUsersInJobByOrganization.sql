SELECT U.ID, U.Name, U.DisplayName, U.CodeName
FROM SC.SchemaRelationObjectsSnapshot_Current R INNER JOIN SC.SchemaGroupSnapshot_Current G ON R.ObjectID = G.ID
	INNER JOIN SC.UserAndContainerSnapshot_Current UC ON G.ID = UC.ContainerID
	INNER JOIN SC.SchemaUserSnapshot_Current U ON UC.UserID = U.ID
WHERE R.FullPath LIKE {0} AND R.ChildSchemaType = 'Groups' AND R.ParentSchemaType = 'Organizations' AND G.Name = {1}