
CREATE VIEW [OM].[v_AssetConsumes]
AS
SELECT  CampusID, CampusName, CustomerID, CustomerCode, CustomerName, AccountID, AssetID, AssetCode, AssetType, 
                   AssetRefType, AssetRefPID, AssetRefID, AssetMoney, ConsumeID, ConsumeType, ConsumeMoney, ConsumeMemo, 
                   ConsumeTime, ConsultantID, ConsultantName, ConsultantJobID, EducatorID, EducatorName, EducatorJobID, NULL 
                   AS TeacherID, NULL AS TeacherName, NULL AS TeacherJobID, NULL AS StartTime, NULL AS EndTime, NULL AS DurationValue, Amount, Price, CreatorID, 
                   CreatorName, CreateTime
FROM      OM.AssetConsumes
UNION ALL
SELECT  [CampusID], [CampusName], [CustomerID], [CustomerCode], [CustomerName], [AccountID], [AssetID], [AssetCode], 
                   [AssetType], [AssetRefType], [AssetRefPID], [AssetRefID], [AssetMoney], [ConfirmID], [AssetType], 
                   [ConfirmMoney] * [ConfirmFlag], [ConfirmMemo], [ConfirmTime], [ConsultantID], [ConsultantName], [ConsultantJobID], 
                   [EducatorID], [EducatorName], [EducatorJobID], [TeacherID], [TeacherName], [TeacherJobID], [StartTime], [EndTime], 
                   [DurationValue], [Amount], [Price], [CreatorID], [CreatorName], [CreateTime]
FROM      [OM].[AssetConfirms]

GO