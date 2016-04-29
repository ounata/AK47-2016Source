
CREATE VIEW [OM].[v_OrderItems]
AS
SELECT  OM.Orders.CampusID, OM.Orders.CampusName, OM.Orders.ParentID, OM.Orders.ParentName, OM.Orders.CustomerID, 
                   OM.Orders.CustomerCode, OM.Orders.CustomerName, OM.Orders.CustomerGrade, OM.Orders.CustomerGradeName, 
                   OM.Orders.AccountID, OM.Orders.AccountCode, OM.Orders.OrderID, OM.Orders.OrderNo, OM.Orders.OrderTime, 
                   OM.Orders.OrderKind, OM.Orders.OrderType, OM.Orders.OrderStatus, OM.Orders.ProcessStatus, 
                   OM.Orders.ProcessTime, OM.Orders.ProcessMemo, OM.Orders.ConsultantID, OM.Orders.ConsultantName, 
                   OM.Orders.ConsultantJobID, OM.Orders.EducatorID, OM.Orders.EducatorName, OM.Orders.EducatorJobID, 
                   OM.Orders.SubmitterID, OM.Orders.SubmitterName, OM.Orders.SubmitterJobID, OM.Orders.SubmitterJobName, 
                   OM.Orders.SubmitterJobType, OM.Orders.SubmitTime, OM.Orders.ApproverID, OM.Orders.ApproverName, 
                   OM.Orders.ApproverJobID, OM.Orders.ApproverJobName, OM.Orders.ApproveTime, OM.Orders.ChargeApplyID, 
                   OM.Orders.CreatorID, OM.Orders.CreatorName, OM.Orders.CreateTime, OM.Orders.ModifierID, OM.Orders.ModifierName, 
                   OM.Orders.ModifyTime, OM.OrderItems.SortNo, OM.OrderItems.ItemID, OM.OrderItems.ItemNo, OM.OrderItems.ProductID, 
                   OM.OrderItems.ProductCode, OM.OrderItems.ProductName, OM.OrderItems.ProductUnit, 
                   OM.OrderItems.ProductUnitName, OM.OrderItems.Grade, OM.OrderItems.GradeName, OM.OrderItems.Subject, 
                   OM.OrderItems.SubjectName, OM.OrderItems.Catalog, OM.OrderItems.CatalogName, OM.OrderItems.CategoryType, 
                   OM.OrderItems.CategoryTypeName, OM.OrderItems.CourseLevel, OM.OrderItems.CourseLevelName, 
                   OM.OrderItems.LessonDuration, OM.OrderItems.LessonDurationValue, OM.OrderItems.ProductCampusID, 
                   OM.OrderItems.ProductCampusName, OM.OrderItems.OrderPrice, OM.OrderItems.OrderAmount, 
                   OM.OrderItems.PresentID, OM.OrderItems.PresentQuato, OM.OrderItems.PresentAmount, OM.OrderItems.TunlandRate, 
                   OM.OrderItems.SpecialRate, OM.OrderItems.DiscountType, OM.OrderItems.DiscountRate, OM.OrderItems.RealPrice, 
                   OM.OrderItems.RealAmount, OM.OrderItems.PromotionQuota, OM.OrderItems.ExpirationDate, 
                   OM.OrderItems.JoinedClassID, OM.OrderItems.RelatedAssetID, OM.OrderItems.RelatedAssetCode, OM.Assets.Amount, 
                   OM.Assets.Price
FROM      OM.Orders INNER JOIN
                   OM.OrderItems ON OM.Orders.OrderID = OM.OrderItems.OrderID LEFT OUTER JOIN
                   OM.Assets ON OM.OrderItems.ItemID = OM.Assets.AssetRefID

GO
