﻿using System;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Products.Executors
{
    public class PPTSProductExecutor : PPTSExecutorBase
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        /// <param name="opType"></param>
        public PPTSProductExecutor(string opType) :
            base(opType)
        {
        }



        public string ProductId { set; get; }

        /// <summary>
        /// 启售时间
        /// </summary>
        public DateTime Date { set; get; }

        public string[] ProductIds { set; get; }


        public Action<ProductView, ProductExOfCourse, ProductSalaryRuleCollection, ProductPermissionCollection> FillProductView { set; get; }
        public Action<Product, ProductExOfCourse, ProductSalaryRuleCollection, ProductPermissionCollection> FillProduct { set; get; }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {

            if (OperationType == "StopSell") { return ProductAdapter.Instance.StopSellProduct(ProductId); }
            if (OperationType == "DelaySell") { return ProductAdapter.Instance.DelaySellProduct(ProductId, Date); }
            //if (OperationType == "StartSell") { return ProductAdapter.Instance.StartSellProduct(ProductId, Date); }

            if (OperationType == "GetProducts")
            {
                return ProductViewAdapter.Instance.LoadByInBuilder(new MCS.Library.Data.Adapters.InLoadingCondition() { DataField = "ProductId", BuilderAction = (builder) => { builder.AppendItem(ProductIds); } });
            }

            if (OperationType == "GetProductView")
            {

                ProductView productView = null;
                ProductExOfCourse exOfCourse = null;
                ProductSalaryRuleCollection salaryRules = null;
                ProductPermissionCollection permissions = null;

                ProductViewAdapter.Instance.LoadByProductIDInContext(ProductId, collection =>
                {
                    productView = collection.FirstOrNull();
                });
                ProductExOfCourseAdapter.Instance.LoadByProductIDInContext(ProductId, collection =>
                {
                    exOfCourse = collection.FirstOrNull();
                });
                ProductSalaryRuleAdapter.Instance.LoadByProductIDInContext(ProductId, collection => { salaryRules = collection; });

                ProductPermissionAdapter.Instance.LoadByProductIDInContext(ProductId, collection => { permissions = collection; });

                using (var currentContext = ProductExOfCourseAdapter.Instance.GetDbContext())
                {
                    currentContext.ExecuteDataSetSqlInContext();
                }
                FillProductView(productView, exOfCourse, salaryRules, permissions);

                return true;
            }

            if (OperationType == "DelProduct")
            {

                ProductAdapter.Instance.DeleteInContext(builder => builder.AppendItem("ProductID", ProductId));
                ProductExOfCourseAdapter.Instance.DeleteInContext(builder => builder.AppendItem("ProductID", ProductId));
                ProductSalaryRuleAdapter.Instance.DeleteInContext(builder => builder.AppendItem("ProductID", ProductId));

                using (var currentContext = ProductAdapter.Instance.GetDbContext())
                {
                    currentContext.ExecuteNonQuerySqlInContext();
                }
                return true;
            }

            if (OperationType == "GetProduct")
            {

                Product product = null;
                ProductExOfCourse exOfCourse = null;
                ProductSalaryRuleCollection salaryRules = null;
                ProductPermissionCollection permissions = null;

                ProductAdapter.Instance.LoadInContext(ProductId, entity =>
                {
                    product = entity;
                });
                ProductExOfCourseAdapter.Instance.LoadByProductIDInContext(ProductId, collection =>
                {
                    exOfCourse = collection.FirstOrNull();
                });
                ProductSalaryRuleAdapter.Instance.LoadByProductIDInContext(ProductId, collection =>
                {
                    salaryRules = collection;
                });

                ProductPermissionAdapter.Instance.LoadByProductIDInContext(ProductId, collection => { permissions = collection; });

                using (var currentContext = ProductAdapter.Instance.GetDbContext())
                {
                    currentContext.ExecuteDataSetSqlInContext();
                }
                FillProduct(product, exOfCourse, salaryRules, permissions);

                return true;
            }
            return false;
        }

        protected override string GetOperationDescription()
        {
            string description = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return OperationType;
            }
            return description;
        }
        protected override void PersistOperationLogInContext(DataExecutionContext<UserOperationLogCollection> context)
        {
            context.Logs.ForEach(log => ProductUserOperationLogAdapter.Instance.InsertDataInContext(log));
        }
    }
}