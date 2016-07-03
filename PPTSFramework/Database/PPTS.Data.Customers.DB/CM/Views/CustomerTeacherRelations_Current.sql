CREATE VIEW [CM].[CustomerTeacherRelations_Current]
WITH SCHEMABINDING 
AS
SELECT [ID]
	  ,[CustomerID]
      ,[TeacherID]
      ,[TeacherName]
      ,[TeacherJobID]
	  ,[TeacherOACode]
      ,[TeacherJobOrgID]
      ,[TeacherJobOrgName]
      ,[CreatorID]
      ,[CreatorName]
      ,[CreateTime]
      ,[TenantCode]
      ,[VersionStartTime]
      ,[VersionEndTime]
FROM      CM.CustomerTeacherRelations
WHERE  (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))
