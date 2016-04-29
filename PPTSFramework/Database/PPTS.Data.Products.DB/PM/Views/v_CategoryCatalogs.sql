
CREATE VIEW [PM].[v_CategoryCatalogs]
AS
SELECT  PM.Categories.CategoryID, PM.Categories.Category, PM.Categories.CategoryName, PM.Categories.CategoryType, 
                   PM.Categories.HasCourse, PM.Categories.CanInput, PM.CategoryCatalogs.CatalogID, PM.CategoryCatalogs.Catalog, 
                   PM.CategoryCatalogs.CatalogName, PM.CategoryCatalogs.HasPartner, PM.CategoryCatalogs.Eanbled, 
                   PM.CategoryCatalogs.SortNo, PM.CategoryCatalogs.CreatorID, PM.CategoryCatalogs.CreatorName, 
                   PM.CategoryCatalogs.CreateTime, PM.CategoryCatalogs.ModifierID, PM.CategoryCatalogs.ModifierName, 
                   PM.CategoryCatalogs.ModifyTime
FROM      PM.Categories INNER JOIN
                   PM.CategoryCatalogs ON PM.Categories.CategoryID = PM.CategoryCatalogs.CategoryID

GO