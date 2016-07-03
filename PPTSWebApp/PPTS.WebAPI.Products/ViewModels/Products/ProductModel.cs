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
using MCS.Web.MVC.Library.Models.Workflow;
using MCS.Web.MVC.Library.ApiCore;
using PPTS.WebAPI.Products.Common;
using PPTS.Data.Common;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Products.ViewModels.Products
{
    [Serializable]
    public class ProductModel
    {

        #region ViewModel
        public CategoryCatalogCollection Catalogs { set; get; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { private set; get; }

        public static ProductModel ToCreateableProductModel(CategoryType categoryType)
        {
            var model = new ProductModel()
            {
                CategoryType = categoryType,
                Product = new Product() { TunlandAllowed = 0 },
                ExOfCourse = new ProductExOfCourse() { IncomeBelonging = "1", IsCrossCampus = 0 },
                SalaryRules = new ProductSalaryRuleCollection()
            };

            switch (categoryType)
            {
                case CategoryType.OneToOne:
                    model.Product.ProductUnit = ProductUnit.Period;
                    model.Product.TunlandAllowed = 1;
                    break;
                case CategoryType.CalssGroup:
                    model.Product.ProductUnit = ProductUnit.Lesso;
                    break;
                case CategoryType.YouXue:
                case CategoryType.WuKeShou:
                case CategoryType.Other:
                    model.Product.ProductUnit = ProductUnit.Issue;
                    break;
                case CategoryType.Abroad:
                case CategoryType.Cost:
                case CategoryType.Real:
                case CategoryType.Virtual:
                    model.Product.ProductUnit = ProductUnit.Part;
                    break;

            }

            model.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(
                    typeof(Product),
                    typeof(ProductExOfCourse),
                    typeof(ProductSalaryRule)
                );

            model.Catalogs = CategoryCatalogAdapter.Instance.LoadByCategoryType(categoryType);
            model.Product.Catalog = model.Catalogs.First().Catalog;

            //权限过滤
            var currentUser = MCS.Library.Principal.DeluxeIdentity.CurrentUser;
            if (currentUser != null && !currentUser.HasInternationalYouXue())
            {
                model.Catalogs = model.Catalogs.Where(m => m.Category != "32");
            }


            return model;
        }

        #endregion




        public string[] CampusIDs { set; get; }
        public CategoryType CategoryType { set; get; }
        public string ProductCodePrefix { set; get; }

        [ObjectValidator]
        public Product Product { set; get; }

        [ObjectValidator]
        public ProductExOfCourse ExOfCourse { set; get; }

        [ObjectValidator]
        public ProductSalaryRuleCollection SalaryRules { set; get; }

        [ObjectValidator]
        public ProductPermissionCollection Permissions { set; get; }

        public ProductModel FillProduct()
        {
            if (Product.ProductID.IsNullOrWhiteSpace())
            {
                Product.ProductID = UuidHelper.NewUuidString();
            }
            if (Product.ProductCode.IsNullOrWhiteSpace())
            {
                Product.ProductCode = Data.Products.Helper.GetProductCode("PT" + ProductCodePrefix);
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
            ExOfCourse.IsNotNull((mm) =>
            {
                var dictionary = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
                ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(ProductView)).ForEach(m => { dictionary.Add(m.Key.ToLower(), m.Value); });
                var GetNameByKey = new Func<string, string, string>((s, v) =>
                {
                    s = s.ToLower();
                    var fname = s.StartsWith("c_code") ? s : "c_code_abbr_product_" + s;
                    var dv = dictionary[fname].SingleOrDefault(m => m.Key == v);
                    var rv = dv == null ? null : dv.Value.Replace("分钟","");
                    return rv;
                });

                mm.LessonDurationValue = Convert.ToDecimal(GetNameByKey("c_codE_ABBR_BO_ProductDuration", mm.LessonDuration.ToString()));
                mm.PeriodDurationValue = Convert.ToInt32(GetNameByKey("c_codE_ABBR_BO_ProductDuration", mm.PeriodDuration));

            });
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
            (Product.TargetPrice < 0 || Product.ProductPrice < 0 || Product.ProductCost < 0).TrueThrow("提交数据有误！");
            Permissions.Any(m => m.CampusID == "-1").TrueThrow("销售范围 没有选择校区！");

            (CategoryType == CategoryType.YouXue).TrueAction(() =>
            {
                var catalog = CategoryCatalogAdapter.Instance.LoadByCatalog(Product.Catalog);
                catalog.NullCheck("未找到产品类型！");
                (catalog.Category == "32" && !CurrentUser.HasInternationalYouXue()).TrueThrow("当前用户 不可以创建国际游学产品！");
            });



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

        #region 工作流

        public void DoApprove()
        {

            string wfName = WorkflowNames.Product;
            WorkflowHelper wfHelper = new WorkflowHelper(wfName, DeluxeIdentity.CurrentUser);

            if (wfHelper.CheckWorkflow(true))
            {
                var param = new WorkflowStartupParameter()
                {
                    ResourceID = this.Product.ProductID,
                    TaskTitle = string.Format("新建产品 {0}({1})", this.Product.ProductName, this.Product.ProductCode),
                    TaskUrl = "/PPTSWebApp/PPTS.Portal/#/ppts/product/approve"//?processID&activityID&resourceID
                };

                wfHelper.StartupWorkflow(param);
            }

        }

        #endregion
    }
}