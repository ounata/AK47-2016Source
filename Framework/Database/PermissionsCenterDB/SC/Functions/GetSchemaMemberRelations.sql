CREATE FUNCTION [SC].[GetSchemaMemberXml]
(
	@ContainerID NVARCHAR(36),
	@MemberID NVARCHAR(36),
	@ContainerSchemaType NVARCHAR(32),
	@MemberSchemaType NVARCHAR(32),
	@InnerSort BIGINT
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	DECLARE @tempTable TABLE(ContainerID NVARCHAR(36), SchemaType NVARCHAR(32), [ID] NVARCHAR(36), ContainerSchemaType NVARCHAR(32), MemberSchemaType NVARCHAR(32), InnerSort BIGINT)

	INSERT INTO @tempTable(ContainerID, SchemaType, [ID] , ContainerSchemaType, MemberSchemaType, InnerSort)
	SELECT @ContainerID, 'MemberRelations', @MemberID, @ContainerSchemaType, @MemberSchemaType, @InnerSort

	SELECT @result = 
		(SELECT * FROM @tempTable AS [Object] FOR XML AUTO)

	RETURN @result
END
