using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.Executors
{
    [DataExecutorDescription("删除折扣表")]
    public class DeleteDiscountExecutor : PPTSEditProductExecutorBase<String>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public DeleteDiscountExecutor(String model)
            : base(model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Discount d = DiscountAdapter.Instance.Load(builder=>builder.AppendItem("DiscountID",Model)).FirstOrDefault();
            if (d == null || d.DiscountStatus != Data.Products.DiscountStatusDefine.Refused) {
                throw new Exception("只有驳回状态的折扣表可以删除!");
            }
            d.DiscountStatus = Data.Products.DiscountStatusDefine.Deleted;
            DiscountAdapter.Instance.Update(d);
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
        }
    }
}