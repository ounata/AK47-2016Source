using PPTS.Data.Common.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Orders.Adapters;

namespace PPTS.Data.Orders.Executors
{
    public class PPTSClassGroupExecutor : PPTSExecutorBase
    {

        /// <summary>
        /// 操作类型
        /// </summary>
        /// <param name="opType"></param>
        public PPTSClassGroupExecutor(string opType) :
            base(opType)
        {
        }

        public string ProductId { set; get; }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {

            if (this.OperationType == "GetClassesByProductId")
            {
                return ClassesAdapter.Instance.Load(w => w.AppendItem("ProductID", ProductId));
            }


            throw new NotImplementedException();
        }


        protected override string GetOperationDescription()
        {
            var descrption = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(descrption)) { return OperationType; }
            return descrption;
        }



    }
}
