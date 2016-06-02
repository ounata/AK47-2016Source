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
    [DataExecutorDescription("停用买赠表")]
    public class DisablePresentExecutor : PPTSEditProductExecutorBase<String>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public DisablePresentExecutor(String model)
            : base(model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Present d = PresentAdapter.Instance.Load(builder => builder.AppendItem("PresentID", Model)).FirstOrDefault();
            if (d == null || (d.PresentStatus != Data.Products.PresentStatusDefine.Approved && d.PresentStatus != Data.Products.PresentStatusDefine.Enabled))
            {
                throw new Exception("只有执行中和审批通过状态的买赠表可以停用!");
            }
            d.PresentStatus = Data.Products.PresentStatusDefine.Disabled;
            PresentAdapter.Instance.UpdateInContext(d);

            Data.Products.Entities.PresentPermissionViewCollection dpv = PresentPermissionViewAdapter.Instance.Load(builder => builder.AppendItem("PresentID", Model));
            PresentPermissionCollection dpc = PresentPermissionAdapter.Instance.Load(builder => builder.AppendItem("PresentID", Model));
            foreach (var item in dpc)
            {
                var dpv_ = dpv.Find(dp => dp.CampusID == item.CampusID && dp.PresentID == item.PresentID);
                if (dpv_ != null)
                {
                    item.EndDate = SNTPClient.AdjustedTime;
                    PresentPermissionAdapter.Instance.UpdateInContext(item);
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