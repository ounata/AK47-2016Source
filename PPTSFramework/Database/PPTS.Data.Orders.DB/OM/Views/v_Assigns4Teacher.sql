
CREATE VIEW [OM].[v_Assigns4Teacher]
AS
SELECT  AssignID, AssignTime, AssignStatus, CampusID, CampusName, AssignPrice, AssignSource, AssignMemo, CopyAllowed, 
                   ConfirmID, ConfirmTime, ConfirmStatus, ConfirmPrice, AssetID, AssetCode, CustomerID, AccountID, CustomerCode, 
                   CustomerName, ProductID, ProductCode, ProductName, RoomID, RoomCode, RoomName, TeacherID, TeacherName, 
                   TeacherJobID, TeacherJobOrgID,TeacherJobOrgName, IsFullTimeTeacher, ConsultantID, ConsultantName, ConsultantJobID, EducatorID, EducatorName, EducatorJobID, Grade, 
                   GradeName, Subject, SubjectName, DurationValue, Amount, StartTime, EndTime, CreatorID, CreatorName, CreateTime, 
                   ModifierID, ModifierName, ModifyTime
FROM      OM.Assigns
UNION ALL
SELECT  [AssignID], [AssignTime], [AssignStatus], [CampusID], 
                   [CampusName], NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,
                    NULL, NULL, NULL, [TeacherID], [TeacherName], [TeacherJobID], TeacherJobOrgID,TeacherJobOrgName, IsFullTimeTeacher, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 
                   '18', '陪读', NULL, [Amount], StartTime, EndTime, CreatorID, CreatorName, CreateTime, ModifierID, ModifierName, 
                   ModifyTime
FROM      [OM].[AccompanyAssigns]

GO