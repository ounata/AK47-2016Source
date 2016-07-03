
CREATE VIEW [PM].[v_Products]
AS
SELECT  PM.Products.ProductID, PM.Products.ProductCode, PM.Products.ProductName, PM.Products.ProductMemo, 
                   PM.Products.ProductStatus, PM.Products.ProductVersion, PM.Products.ProductPrice, PM.Products.ProductCost, 
                   PM.Products.ProductUnit, PM.Products.TargetPrice, PM.Products.ProductPrice * (1 - ISNULL(PM.Products.PartnerRatio, 0)) 
                   AS CooperationPrice, PM.Products.TargetPriceMemo, PM.v_CategoryCatalogs.CategoryType, 
                   PM.v_CategoryCatalogs.Category, PM.v_CategoryCatalogs.CategoryName, PM.v_CategoryCatalogs.CatalogName, 
                   PM.Products.Catalog, PM.Products.Subject, PM.Products.Grade, PM.Products.Season, 
                   PM.v_CategoryCatalogs.HasCourse, PM.v_CategoryCatalogs.CanInput, PM.v_CategoryCatalogs.HasPartner, 
                   PM.Products.StartDate, PM.Products.EndDate, PM.Products.ConfirmStartDate, PM.Products.ConfirmEndDate, 
                   PM.Products.ConfirmMode, PM.Products.ConfirmStaging, PM.Products.PartnerID, PM.Products.PartnerRatio, 
                   PM.Products.PartnerName, PM.Products.SpecialAllowed, PM.Products.TunlandAllowed, PM.Products.PresentAllowed, 
                   PM.Products.PromotionAllowed, PM.Products.PromotionQuota, PM.ProductsExOfCourse.LessonCount, 
                   PM.ProductsExOfCourse.LessonDuration, PM.ProductsExOfCourse.LessonDurationValue, 
                   PM.ProductsExOfCourse.PeriodDuration, PM.ProductsExOfCourse.PeriodDurationValue, 
                   PM.ProductsExOfCourse.PeriodsOfLesson, PM.ProductsExOfCourse.CourseLevel, PM.ProductsExOfCourse.CoachType, 
                   PM.ProductsExOfCourse.GroupType, PM.ProductsExOfCourse.ClassType, PM.ProductsExOfCourse.MinPeoples, 
                   PM.ProductsExOfCourse.MaxPeoples, PM.ProductsExOfCourse.IncomeBelonging, 
                   PM.ProductsExOfCourse.IsCrossCampus, PM.Products.SubmitterID, PM.Products.SubmitterName, 
                   PM.Products.SubmitterJobID, PM.Products.SubmitterJobName, PM.Products.SubmitTime, PM.Products.CreatorID, 
                   PM.Products.CreatorName, PM.Products.CreateTime, PM.Products.ModifierID, PM.Products.ModifierName, 
                   PM.Products.ModifyTime, PM.Products.RdOrgID, PM.Products.RdOrgName
FROM      PM.v_CategoryCatalogs INNER JOIN
                   PM.Products ON PM.v_CategoryCatalogs.Catalog = PM.Products.Catalog LEFT OUTER JOIN
                   PM.ProductsExOfCourse ON PM.Products.ProductID = PM.ProductsExOfCourse.ProductID


GO
