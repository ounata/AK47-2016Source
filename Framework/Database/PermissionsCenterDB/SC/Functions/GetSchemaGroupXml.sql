﻿CREATE FUNCTION [SC].[GetSchemaGroupXml]
(
	@ID NVARCHAR(36),
	@Name NVARCHAR(255),
	@DisplayName NVARCHAR(255),
	@CodeName NVARCHAR(64),
	@Description NVARCHAR(255),
	@GroupType INT = 0,
	@IsFullTime INT = 1,
	@IsPrimary INT = 1,
	@ParentJobID NVARCHAR(36),
	@GroupFunctions NVARCHAR(MAX) = ''
)
RETURNS XML
AS
BEGIN
	DECLARE @result XML

	DECLARE @tempTable TABLE([ID] NVARCHAR(36), SchemaType NVARCHAR(32), Name NVARCHAR(255), DisplayName NVARCHAR(255), CodeName NVARCHAR(64), [Description] NVARCHAR(255),
		GroupType INT, IsFullTime NVARCHAR(32), IsPrimary NVARCHAR(32), ParentJobID NVARCHAR(36), GroupFunctions NVARCHAR(MAX))

	INSERT INTO @tempTable([ID], SchemaType, Name, DisplayName, CodeName, [Description], GroupType, IsFullTime, IsPrimary, ParentJobID, GroupFunctions)
	SELECT @ID AS [ID],
		'Groups',
		@Name AS Name,
		@DisplayName AS DisplayName,
		@CodeName AS CodeName,
		@Description AS Comment,
		@GroupType AS DepartmentType,
		(CASE WHEN @IsFullTime = 0 THEN 'False' ELSE 'True' END ) AS [IsFullTime],
		(CASE WHEN @IsPrimary = 0 THEN 'False' ELSE 'True' END ) AS [IsPrimary],
		@ParentJobID AS ParentJobID,
		@GroupFunctions AS GroupFunctions

	SELECT @result = 
		(SELECT * FROM @tempTable AS [Object] FOR XML AUTO)

	RETURN @result
END
