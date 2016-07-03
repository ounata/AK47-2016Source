using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Contracts.Proxies;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("同步班级数量到产品")]
    public class SyncClassCountToProductExecutor : PPTSEditClassGroupExecutorBase<string>
    {
        public SyncClassCountToProductExecutor(string ID)
            : base(ID, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Class c = ClassesAdapter.Instance.LoadByClassID(Model);

            PPTSClassServiceProxy.Instance.SyncClassCountToProduct(c.ProductID);
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