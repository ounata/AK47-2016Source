CREATE FUNCTION [SC].[GetSchemaRoleXml]
(
	@ID NVARCHAR(36),
	@Name NVARCHAR(255),
	@DisplayName NVARCHAR(255),
	@CodeName NVARCHAR(64),
	@Description NVARCHAR(255)
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	DECLARE @tempTable TABLE([ID] NVARCHAR(36), SchemaType NVARCHAR(32), Name NVARCHAR(255), DisplayName NVARCHAR(255), CodeName NVARCHAR(64), [Description] NVARCHAR(255))

	INSERT INTO @tempTable([ID], SchemaType, Name, DisplayName, CodeName, [Description])
	SELECT @ID AS [ID],
		'Roles',
		@Name AS Name,
		@DisplayName AS DisplayName,
		@CodeName AS CodeName,
		@Description AS Comment

	SELECT @result = 
		(SELECT * FROM @tempTable AS [Object] FOR XML AUTO)

	RETURN @result
END
