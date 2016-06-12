/*用于学大教育数据同步。 获取组织机构xml数据 */
CREATE FUNCTION [SC].[GetSchemaOrganizationXml]
(
	@ID NVARCHAR(36),
	@Name NVARCHAR(255),
	@DisplayName NVARCHAR(255),
	@CodeName NVARCHAR(64),
	@DepartmentType NVARCHAR(32),
	@ShortName NVARCHAR(64),
	@Address NVARCHAR(MAX),
	@Description NVARCHAR(255),
	@SimplePinyin NVARCHAR(32),
	@FullPinyin NVARCHAR(255)
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	DECLARE @tempTable TABLE([ID] NVARCHAR(36), SchemaType NVARCHAR(32), Name NVARCHAR(255), DisplayName NVARCHAR(255), CodeName NVARCHAR(64), DepartmentType NVARCHAR(32),
		ShortName NVARCHAR(64), [Address] NVARCHAR(MAX), [Description] NVARCHAR(255), [SimplePinyin] NVARCHAR(32), [FullPinyin] NVARCHAR(255))

	INSERT INTO @tempTable([ID], SchemaType, Name, DisplayName, CodeName, DepartmentType, ShortName, [Address], [Description], [SimplePinyin], [FullPinyin])
	SELECT @ID AS [ID],
		'Organizations',
		@Name AS Name,
		@DisplayName AS DisplayName,
		@CodeName AS CodeName,
		@DepartmentType AS DepartmentType,
		@ShortName AS ShortName,
		@Address AS [Address],
		@Description AS Comment,
		@SimplePinyin,
		@FullPinyin

	SELECT @result = 
		(SELECT * FROM @tempTable AS [Object] FOR XML AUTO)

	RETURN @result
END
