using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.DataObjects.AsyncTransactional;
using MCS.Library.Test.Data.Adapters;
using MCS.Library.Test.Data.Entities;
using MCS.Library.WcfExtensions;
using MCS.Services.Test.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Transactions;

namespace MCS.Services.Test.Services
{
    public class RepertoryTransactionService : IRepertoryTransactionService
    {
        /// <summary>
        /// 从全局拷贝到本地后
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="repertoryID"></param>
        /// <param name="changeQuantity"></param>
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ChangeUsedQuantity(string processID, string repertoryID, int changeQuantity)
        {
            RepertoryAdapter.GetInstance(Common.RepertoryDB).Load(repertoryID).IsNotNull(repertory =>
            {
                TxProcessExecutor.GetExecutor(processID).
                    PrepareData(process => process.CurrentActivity.Context["UsedQuantity"] = repertory.UsedQuantity).
                    ExecuteMoveTo(process => RepertoryAdapter.GetInstance(process.CurrentActivity.ConnectionName).ChangeUsedQuantity(repertoryID, changeQuantity));
            });
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void RollbackUsedQuantity(string processID, string repertoryID)
        {
            RepertoryAdapter.GetInstance(Common.RepertoryDB).Load(repertoryID).IsNotNull(repertory =>
            {
                TxProcessExecutor.GetExecutor(processID).ExecuteRollback(process =>
                {
                    int oldUsedQuantity = process.CurrentActivity.Context.GetValue("UsedQuantity", -1);

                    if (oldUsedQuantity >= 0)
                        RepertoryAdapter.GetInstance(Common.RepertoryDB).SetUsedQuantity(repertoryID, oldUsedQuantity);
                });
            });
        }
    }
}
