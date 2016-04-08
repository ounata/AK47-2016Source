/* 生成一个Schema表的快照数据 */
CREATE PROCEDURE [SC].[GenerateSchemaTableSnapshot]
	@schemaName NVARCHAR(128),
	@snapshotFieldMask INT,
	@tableName NVARCHAR(255),
	@snapshotTableName NVARCHAR(255),
	@timePoint DATETIME = NULL
AS
BEGIN
	IF (@snapshotTableName IS NOT NULL)
	BEGIN
		DECLARE @deleteSql NVARCHAR(MAX)

		SET @deleteSql = 'DELETE ' + @snapshotTableName + ' WHERE SchemaType = ''' + @schemaName + ''''

		IF (@timePoint IS NOT NULL)
			SET @deleteSql = @deleteSql + ' AND VersionStartTime >= ''' + CONVERT(NVARCHAR(32), @timePoint, 121) + ''''
			
		DECLARE @sql NVARCHAR(MAX)

		SET @sql = SC.GetInsertSchemaTableSnapshotSql(@schemaName, @snapshotFieldMask, @tableName, @snapshotTableName, @timePoint)

		PRINT @deleteSql
		PRINT CONVERT(NVARCHAR(32), GETDATE(), 121)
		EXECUTE dbo.sp_executesql @deleteSql

		PRINT @sql
		PRINT CONVERT(NVARCHAR(32), GETDATE(), 121)

		EXECUTE dbo.sp_executesql @sql
	END
END
