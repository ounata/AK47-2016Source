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
	  ,[TeacherJobOrgShortName]
      ,[TeacherJobOrgName]
      ,[CreatorID]
      ,[CreatorName]
      ,[CreateTime]
      ,[VersionStartTime]
      ,[VersionEndTime]
FROM      CM.CustomerTeacherRelations
WHERE  (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))
