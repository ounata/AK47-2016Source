using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using System;

namespace PPTS.Data.Orders.Executors
{
    public abstract class PPTSEditPurchaseExecutorBase<TModel> : PPTSEditEntityExecutorBase<TModel>
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public PPTSEditPurchaseExecutorBase(TModel model, Action<TModel> dataAction) :
            base(model, dataAction)
        {
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        /// <param name="opType"></param>
        public PPTSEditPurchaseExecutorBase(TModel model, Action<TModel> dataAction, string opType) :
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
