using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PPTS.Data.Common.Security;
using MCS.Library.Core;


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

        public string[] CampusIDs { set; get; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { private set; get; }



        public ProductModel FillProduct()
        {
            if (Product.ProductID.IsNullOrWhiteSpace())
            {
                Product.ProductID = UuidHelper.NewUuidString();
            }
            if (CategoryType == CategoryType.OneToOne)
            {
                Product.StartDate = Product.EndDate = DateTime.Parse("3000-12-31");
            }
            Product.ProductStatus = ProductStatus.PendingApproval;

            FillUser(Product);
            return this;
        }

        public ProductModel FillExOfCourse()
        {

            return this;
        }

        public ProductModel FillSalaryRules()
        {
            if (SalaryRules != null && SalaryRules.Count > 0)
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
            CampusIDs.IsNotNull(campusIds =>
            {
                campusIds.ToList()
                .ForEach(s =>
                            {
                                var model = new ProductPermission() { ProductID = this.Product.ProductID, CampusID = s };
                                FillUser(model);
                                Permissions.Add(model);
                            });
            });
            return this;
        }


        public void Validate()
        {

        }


        public static ProductModel ToCreateableProductModel(CategoryType categoryType)
        {
            var model = new ProductModel()
            {
                CategoryType = categoryType,
                Product = new Product() { TunlandAllowed = 0 },
                ExOfCourse = new ProductExOfCourse() { IncomeBelonging = "0", IsCrossCampus = 0 },
                SalaryRules = new ProductSalaryRuleCollection()
            };

            switch (categoryType)
            {
                case CategoryType.OneToOne:
                    model.Product.ProductUnit = ProductUnit.Period;
                    model.Product.TunlandAllowed = 1;
                    break;
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


        public MCS.Library.OGUPermission.IUser CurrentUser { set; get; }

        private void FillUser(object info)
        {
            if (CurrentUser != null)
            {
                if (info is IEntityWithCreator)
                {
                    CurrentUser.FillCreatorInfo(info as IEntityWithCreator);
                }
                if (info is IEntityWithModifier)
                {
                    CurrentUser.FillModifierInfo(info as IEntityWithModifier);
                }
                if (info is Product)
                {
                    var model = info as Product;
                    model.SubmitterID = model.CreatorID;
                    model.SubmitterName = model.CreatorName;
                    model.SubmitterJobID = CurrentUser.GetCurrentJob().ID;
                    model.SubmitterJobName = CurrentUser.GetCurrentJob().Name;
                }
            }

        }


    }
}