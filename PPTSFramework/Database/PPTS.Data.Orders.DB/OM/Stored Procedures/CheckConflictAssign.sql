/*排课冲突检查*/
CREATE PROCEDURE [OM].[CheckConflictAssign]
	@assignID NVARCHAR(36),   /*排课ID*/
	@customerID NVARCHAR(36), /*学员ID*/
	@teacherID NVARCHAR(36),  /*认课教师ID*/
    @startTime DATETIME,      /*安排的上课时间*/
	@endTime DATETIME         /*安排的上课时间*/
AS
	IF EXISTS(SELECT AssignID FROM Assigns WHERE (DATEADD(hour,DATEDIFF(hour,GETDATE(),GETUTCDATE()),@startTime) < EndTime and DATEADD(hour,DATEDIFF(hour,GETDATE(),GETUTCDATE()),@endTime) > StartTime) AND CustomerID=@customerID  AND AssignID<>@assignID AND AssignStatus<>10)
		BEGIN;
			RAISERROR ('添加课表失败，学员时间已被占用！', 16, 1) WITH NOWAIT;		
		END;

	IF EXISTS(SELECT AssignID FROM Assigns WHERE (DATEADD(hour,DATEDIFF(hour,GETDATE(),GETUTCDATE()),@startTime) < EndTime and DATEADD(hour,DATEDIFF(hour,GETDATE(),GETUTCDATE()),@endTime) > StartTime) AND TeacherID=@teacherID AND AssignID<>@assignID  AND AssignStatus<>10)
		BEGIN;
			RAISERROR ('添加课表失败，教师的时间已被占用！', 16, 1) WITH NOWAIT;
		END;
