
CREATE VIEW [PM].[v_ProductClassStats]
AS
SELECT  PM.Products.ProductID, PM.Products.ProductCode, PM.Products.ProductName, PM.Products.ProductMemo, 
                   PM.Products.ProductStatus, PM.Products.ProductPrice, PM.Products.ProductCost, PM.Products.ProductUnit, 
                   PM.Products.TargetPrice, PM.Products.TargetPriceMemo, PM.Products.Subject, PM.Products.Grade, PM.Products.Season, 
                   PM.Products.StartDate, PM.Products.EndDate, PM.Products.PartnerID, PM.Products.PartnerRatio, 
                   PM.Products.PartnerName, PM.Products.SpecialAllowed, PM.Products.TunlandAllowed, PM.Products.PresentAllowed, 
                   PM.Products.PromotionAllowed, PM.Products.PromotionQuota, PM.Products.ConfirmStartDate, 
                   PM.Products.ConfirmEndDate, PM.Products.SubmitterID, PM.Products.SubmitterName, PM.Products.SubmitterJobID, 
                   PM.Products.SubmitterJobName, PM.Products.SubmitTime, PM.Products.ApproverID, PM.Products.ApproverName, 
                   PM.Products.ApproverJobID, PM.Products.ApproverJobName, PM.Products.ApproveTime, PM.Products.CreatorID, 
                   PM.Products.CreatorName, PM.Products.CreateTime, PM.Products.ModifierID, PM.Products.ModifierName, 
                   PM.Products.ModifyTime, PM.ProductsExOfCourse.LessonCount, PM.ProductsExOfCourse.LessonDuration, 
                   PM.ProductsExOfCourse.LessonDurationValue, PM.ProductsExOfCourse.PeriodDuration, 
                   PM.ProductsExOfCourse.PeriodDurationValue, PM.ProductsExOfCourse.PeriodsOfLesson, 
                   PM.ProductsExOfCourse.CourseLevel, PM.ProductsExOfCourse.CoachType, PM.ProductsExOfCourse.GroupType, 
                   PM.ProductsExOfCourse.ClassType, PM.ProductsExOfCourse.MinPeoples, PM.ProductsExOfCourse.MaxPeoples, 
                   PM.ProductsExOfCourse.IncomeBelonging, PM.ProductsExOfCourse.IsCrossCampus, PM.ProductClassStats.ClassCount, 
                   PM.ProductClassStats.ValidClasses, PM.CategoryCatalogs.CategoryID, PM.CategoryCatalogs.CatalogID, 
                   PM.CategoryCatalogs.Catalog, PM.CategoryCatalogs.CatalogName
FROM      PM.Products INNER JOIN
                   PM.ProductClassStats ON PM.Products.ProductID = PM.ProductClassStats.ProductID INNER JOIN
                   PM.ProductsExOfCourse ON PM.Products.ProductID = PM.ProductsExOfCourse.ProductID INNER JOIN
                   PM.CategoryCatalogs ON PM.Products.Catalog = PM.CategoryCatalogs.Catalog

GO