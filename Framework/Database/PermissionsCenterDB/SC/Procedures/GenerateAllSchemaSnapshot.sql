/* 生成所有快照数据，此操作将清空所有权限中心快照表并重新生成其中的数据 */

CREATE PROCEDURE [SC].[GenerateAllSchemaSnapshot]
	@timePoint DATETIME = NULL
AS
	SET NOCOUNT ON;

	IF (@timePoint IS NULL)
	BEGIN
		--Clear Tables Begin
		DELETE SC.SchemaApplicationSnapshot
		DELETE SC.SchemaGroupSnapshot
		DELETE SC.SchemaOrganizationSnapshot
		DELETE SC.SchemaUserSnapshot
		DELETE SC.SchemaMembersSnapshot
		DELETE SC.SchemaPermissionSnapshot
		DELETE SC.SchemaRelationObjectsSnapshot
		DELETE SC.SchemaRoleSnapshot

		DELETE SC.SchemaObjectSnapshot
		DELETE SC.UserAndContainerSnapshot
	END
	--Clear Table End

	--Generate Snapshot Begin
	EXECUTE SC.GenerateSchemaSnapshot 'Applications', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'Groups', @timePoint
	EXECUTE SC.GenerateUserAndContainerSnapshot 'Groups', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'MemberRelations', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'Organizations', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'Permissions', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'RelationObjects', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'Roles', @timePoint
	EXECUTE SC.GenerateUserAndContainerSnapshot 'Roles', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'SecretaryRelations', @timePoint
	EXECUTE SC.GenerateSchemaSnapshot 'Users', @timePoint
	--Generate Snapshot End	
RETURN 0
