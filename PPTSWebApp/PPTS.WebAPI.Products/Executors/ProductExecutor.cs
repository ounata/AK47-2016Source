using System;
using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.Data.Products.Executors;
using PPTS.WebAPI.Products.ViewModels.Products;
using PPTS.Data.Products;

namespace PPTS.WebAPI.Products.Executors
{
    /// <summary>
    /// 
    /// </summary>
    [DataExecutorDescription("增加产品")]
    public class ProductExecutor : PPTSEditProductExecutorBase<ProductModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public ProductExecutor(ProductModel model)
            : base(model, null)
        {
            model.NullCheck("model");
            model.Product.NullCheck("Product");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            if (string.IsNullOrWhiteSpace(this.Model.Product.ProductID))
            {
                this.Model.Product.ProductID = Guid.NewGuid().ToString();
            }
            if (this.Model.CategoryType == CategoryType.OneToOne)
            {
                this.Model.Product.StartDate = this.Model.Product.EndDate = DateTime.Parse("3000-12-31");
            }

            ProductAdapter.Instance.UpdateInContext(this.Model.Product);
            if (this.Model.ExOfCourse != null)
            {
                ProductExOfCourseAdapter.Instance.UpdateByProductIDInContext(this.Model.Product.ProductID, new ProductExOfCourseCollection() { this.Model.ExOfCourse });
            }
            if (this.Model.SalaryRules != null && this.Model.SalaryRules.Count > 0)
            {
                
                this.Model.SalaryRules[0].RuleObject = Data.Products.RuleObject.Consultant;
                this.Model.SalaryRules[1].RuleObject = Data.Products.RuleObject.Educator;
                this.Model.SalaryRules[2].RuleObject = Data.Products.RuleObject.Teacher;

                ProductSalaryRuleAdapter.Instance.UpdateByProductIDInContext(this.Model.Product.ProductID, this.Model.SalaryRules);
            }

        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);

            context.Logs.ForEach(log => log.ResourceID = this.Model.Product.ProductID);
        }
    }

}