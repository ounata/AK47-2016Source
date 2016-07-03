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
using MCS.Library.Validation;

namespace PPTS.WebAPI.Products.Executors
{
    /// <summary>
    /// 
    /// </summary>
    [DataExecutorDescription("增加产品")]
    public class AddProductExecutor : PPTSEditProductExecutorBase<ProductModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public AddProductExecutor(ProductModel model)
            : base(model, null)
        {
            model.NullCheck("model");
            model.Product.NullCheck("Product");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            
            this.Model.FillProduct()
                    .FillExOfCourse()
                    .FillSalaryRules()
                    .FillPermissions();
            
            base.PrepareData(context);

            ProductAdapter.Instance.IsExistsSameProductInContext(this.Model.Product);
            ProductAdapter.Instance.UpdateInContext(this.Model.Product);
            if (this.Model.ExOfCourse != null)
            {
                ProductExOfCourseAdapter.Instance.UpdateByProductIDInContext(this.Model.Product.ProductID, this.Model.ExOfCourse);
            }

            ProductSalaryRuleAdapter.Instance.UpdateByProductIDInContext(this.Model.Product.ProductID, this.Model.SalaryRules);
            ProductPermissionAdapter.Instance.UpdateByProductIDInContext(this.Model.Product.ProductID, this.Model.Permissions);
        }

        protected override void Validate()
        {
            Model.Validate();
            base.Validate();
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            var result= base.DoOperation(context);

            Model.DoApprove();

            return result;
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