/*生成用户和容器快照表的快照*/
CREATE PROCEDURE [SC].[GenerateUserAndContainerSnapshot]
	@containerSchemaName NVARCHAR(128),
	@timePoint DATETIME = NULL
AS
BEGIN
	IF (@timePoint IS NULL)
	BEGIN
		INSERT INTO SC.UserAndContainerSnapshot(ContainerID, UserID, ContainerSchemaType, UserSchemaType, VersionStartTime, VersionEndTime, [Status])
		SELECT ContainerID, MemberID, ContainerSchemaType, MemberSchemaType, VersionStartTime, VersionEndTime, [Status]
		FROM SC.SchemaMembers
		WHERE ContainerSchemaType = @containerSchemaName
			AND MemberSchemaType IN
			(SELECT Name
			FROM SC.SchemaDefine
			WHERE IsUsersContainerMember = 1)
	END
	ELSE
	BEGIN
		INSERT INTO SC.UserAndContainerSnapshot(ContainerID, UserID, ContainerSchemaType, UserSchemaType, VersionStartTime, VersionEndTime, [Status])
		SELECT ContainerID, MemberID, ContainerSchemaType, MemberSchemaType, VersionStartTime, VersionEndTime, [Status]
		FROM SC.SchemaMembers
		WHERE ContainerSchemaType = @containerSchemaName
			AND VersionStartTime >= @timePoint
			AND MemberSchemaType IN
			(SELECT Name
			FROM SC.SchemaDefine
			WHERE IsUsersContainerMember = 1)
	END
END
