using System;
using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.Data.Products.Executors;
using PPTS.WebAPI.Product.ViewModels.Products;

namespace PPTS.WebAPI.Product.Executors
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

            ProductAdapter.Instance.UpdateInContext(this.Model.Product);
            if(this.Model.ExOfCourse != null) { 
            ProductExOfCourseAdapter.Instance.UpdateByProductIDInContext(this.Model.Product.ProductID, new ProductExOfCourseCollection() { this.Model.ExOfCourse });
            }
            if(this.Model.SalaryRules !=null && this.Model.SalaryRules.Count>0)
            ProductSalaryRuleAdapter.Instance.UpdateByProductIDInContext(this.Model.Product.ProductID, this.Model.SalaryRules);

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