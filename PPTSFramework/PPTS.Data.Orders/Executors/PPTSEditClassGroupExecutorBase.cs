using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Executors
{
    public abstract class PPTSEditClassGroupExecutorBase<TModel> : PPTSEditEntityExecutorBase<TModel>
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public PPTSEditClassGroupExecutorBase(TModel model, Action<TModel> dataAction) :
            base(model, dataAction)
        {
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        /// <param name="opType"></param>
        public PPTSEditClassGroupExecutorBase(TModel model, Action<TModel> dataAction, string opType) :
            base(model, dataAction, opType)
        {
        }

        protected override void PersistOperationLogInContext(DataExecutionContext<UserOperationLogCollection> context)
        {
            
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            using (DbContext dbContext = PPTS.Data.Orders.ConnectionDefine.GetDbContext())
            {
                dbContext.ExecuteNonQuerySqlInContext();

                if (this.DataAction != null)
                    this.DataAction(this.Model);
            }

            return this.Model;
        }
    }
}
