using MCS.Library.Data.Executors;
using MCS.Library.Net.SNTP;
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
    [DataExecutorDescription("停用折扣表")]
    public class DisableDiscountExecutor : PPTSEditProductExecutorBase<String>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public DisableDiscountExecutor(String model)
            : base(model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Discount d = DiscountAdapter.Instance.Load(builder => builder.AppendItem("DiscountID", Model)).FirstOrDefault();
            if (d == null || (d.DiscountStatus != Data.Products.DiscountStatusDefine.Approved && d.DiscountStatus != Data.Products.DiscountStatusDefine.Enabled))
            {
                throw new Exception("只有执行中和审批通过状态的折扣表可以停用!");
            }
            d.DiscountStatus = Data.Products.DiscountStatusDefine.Disabled;
            DiscountAdapter.Instance.UpdateInContext(d);

            Data.Products.Entities.DiscountPermissionViewCollection dpv = DiscountPermissionViewAdapter.Instance.Load(builder => builder.AppendItem("DiscountID", Model));
            DiscountPermissionCollection dpc = DiscountPermissionAdapter.Instance.Load(builder=>builder.AppendItem("DiscountID", Model));
            foreach (var item in dpc)
            {
                var dpv_ = dpv.Find(dp => dp.CampusID == item.CampusID && dp.DiscountID == item.DiscountID);
                if (dpv_ != null)
                {
                    item.EndDate = SNTPClient.AdjustedTime;
                    DiscountPermissionAdapter.Instance.UpdateInContext(item);
                }
            }
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