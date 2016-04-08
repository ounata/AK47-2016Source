﻿--得到根据Schema插入快照表的INSERT INTO() SELECT ... WHERE SchemaType=...子句
CREATE FUNCTION [SC].[GetInsertSchemaTableSnapshotSql]
(
	@schemaName NVARCHAR(128),
	@snapshotFieldMask INT,
	@tableName NVARCHAR(255),
	@snapshotTableName NVARCHAR(255),
	@timePoint DATETIME = NULL
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	IF @tableName IS NULL OR @tableName = ''
		SET @tableName = 'SC.SchemaObject'

	DECLARE @standardFields NVARCHAR(256)

	SET @standardFields = 'VersionStartTime, VersionEndTime, SchemaType, Status, CreateDate, CreatorID, CreatorName'

	DECLARE @valueFields NVARCHAR(MAX)
	DECLARE @searchValueFields NVARCHAR(MAX)

	SET @searchValueFields = SC.GetSchemaPropertySearchSnapshotFields(@schemaName, 8, ' ')
	SET @valueFields = SC.GetSchemaPropertyValueSnapshotFields(@schemaName, @snapshotFieldMask, ', ')

	DECLARE @selectFields NVARCHAR(MAX)
	DECLARE @allValueFields NVARCHAR(MAX)

	IF (@valueFields <> '')
	BEGIN
		SET @allValueFields = @standardFields + ', ' + @valueFields
		SET @selectFields = @standardFields + ', ' + SC.GetSchemaPropertySnapshotFields(@schemaName, @snapshotFieldMask, ', ')
	END
	ELSE
	BEGIN
		SET @allValueFields = @standardFields
		SET @selectFields = @standardFields
	END
	
	IF (@searchValueFields <> '')
	BEGIN
		SET @allValueFields = @allValueFields + ', ' + @searchValueFields
		SET @selectFields = @selectFields + ', SearchContent'
		END

	DECLARE @sql NVARCHAR(MAX)

	SET @sql = 'INSERT INTO ' + @snapshotTableName + '(' + @selectFields + ') SELECT ' + @allValueFields + ' FROM ' + @tableName + ' WHERE SchemaType = ''' + @schemaName + ''''

	IF (@timePoint IS NOT NULL)
		SET @sql = @sql + ' AND VersionStartTime >= ''' + CONVERT(NVARCHAR(32), @timePoint, 121) + ''''

	RETURN @sql
END
