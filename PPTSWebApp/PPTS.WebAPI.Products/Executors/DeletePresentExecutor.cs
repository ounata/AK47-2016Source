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
    [DataExecutorDescription("删除买赠表")]
    public class DeletePresentExecutor : PPTSEditProductExecutorBase<String>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public DeletePresentExecutor(String model)
            : base(model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Present d = PresentAdapter.Instance.Load(builder => builder.AppendItem("PresentID", Model)).FirstOrDefault();
            if (d == null || d.PresentStatus != Data.Products.PresentStatusDefine.Refused)
            {
                throw new Exception("只有驳回状态的买赠表可以删除!");
            }
            d.PresentStatus = Data.Products.PresentStatusDefine.Deleted;
            PresentAdapter.Instance.Update(d);
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