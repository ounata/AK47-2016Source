using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Products
{
    [Serializable]
    public class ProductModel
    {
        public CategoryCatalogCollection Catalogs { set; get; }
        public CategoryType CategoryType { set; get; }

        [ObjectValidator]
        public Product Product { set; get; }

        [ObjectValidator]
        public ProductExOfCourse ExOfCourse { set; get; }

        [ObjectValidator]
        public ProductSalaryRuleCollection SalaryRules { set; get; }

        [ObjectValidator]
        public ProductPermissionCollection Permissions { set; get; }

        public string [] CampusIDs { set; get; }

        public string CreatorID { set; get; }
        public string CreatorName { set; get; }


        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }


        public static ProductModel ToCreateableProductModel(CategoryType categoryType)
        {
            var model = new ProductModel()
            {
                CategoryType = categoryType,
                Product = new Product(),
                ExOfCourse = new ProductExOfCourse() { IncomeBelonging = "0", IsCrossCampus = 0 },
                SalaryRules = new ProductSalaryRuleCollection()
            };

            switch (categoryType)
            {
                case CategoryType.OneToOne: model.Product.ProductUnit = ProductUnit.Period; break;
            }

            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(
                    typeof(Product),
                    typeof(ProductExOfCourse),
                    typeof(ProductSalaryRule)
                );
            model.Catalogs = CategoryCatalogAdapter.Instance.LoadByCategoryType(categoryType);
            model.Product.Catalog = model.Catalogs.First().Catalog;


            return model;
        }




        public ProductModel FillProduct() {
            if (string.IsNullOrWhiteSpace(Product.ProductID))
            {
                Product.ProductID = Guid.NewGuid().ToString();
            }
            if (CategoryType == CategoryType.OneToOne)
            {
                Product.StartDate = Product.EndDate = DateTime.Parse("3000-12-31");
            }
            Product.ProductStatus = ProductStatus.PendingApproval;

            Product.CreatorID = CreatorID;
            Product.CreatorName = CreatorName;

            return this;
        }

        public ProductModel FillExOfCourse() {
            
            return this;
        }

        public ProductModel FillSalaryRules() {
            if(SalaryRules !=null && SalaryRules.Count > 0)
            {
                SalaryRules[0].RuleObject = Data.Products.RuleObject.Consultant;
                SalaryRules[1].RuleObject = Data.Products.RuleObject.Educator;
                SalaryRules[2].RuleObject = Data.Products.RuleObject.Teacher;
            }
            
            return this;
        }

        public ProductModel FillPermissions()
        {
            Permissions = new ProductPermissionCollection();
            CampusIDs.ToList().ForEach(s => {
                Permissions.Add(new ProductPermission() { ProductID=this.Product.ProductID, CampusID=s, CreatorID= CreatorID, CreatorName=CreatorName });
            });

            return this;
        }
    }
}