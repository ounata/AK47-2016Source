CREATE VIEW [CM].[CustomerTeacherRelations_Current]
WITH SCHEMABINDING 
AS
SELECT [CustomerID]
      ,[TeacherID]
      ,[TeacherName]
      ,[TeacherJobID]
      ,[TeacherJobOrgID]
      ,[TeacherJobOrgName]
      ,[Subject]
      ,[Grade]
      ,[CreatorID]
      ,[CreatorName]
      ,[CreateTime]
      ,[VersionStartTime]
      ,[VersionEndTime]
FROM      CM.CustomerTeacherRelations
WHERE  (VersionEndTime = CONVERT(DATETIME, '99990909 00:00:00', 112))
