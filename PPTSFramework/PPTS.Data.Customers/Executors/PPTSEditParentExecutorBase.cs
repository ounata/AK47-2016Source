using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Validation;
using PPTS.Data.Customers.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PPTS.Data.Common.Executors
{
    /// <summary>
    /// 编辑实体用到的Executor的基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class PPTSEditParentExecutorBase<TModel> : PPTSEditEntityExecutorBase<TModel>
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public PPTSEditParentExecutorBase(TModel model, Action<TModel> dataAction) :
            base(model, dataAction)
        {
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        /// <param name="opType"></param>
        public PPTSEditParentExecutorBase(TModel model, Action<TModel> dataAction, string opType) :
            base(model, dataAction, opType)
        {
        }

        protected override void PersistOperationLogInContext(DataExecutionContext<UserOperationLogCollection> context)
        {
            context.Logs.ForEach(log => CustomerUserOperationLogAdapter.Instance.InsertDataInContext(log));
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            using (DbContext dbContext = PPTS.Data.Customers.ConnectionDefine.GetDbContext())
            {
                this.ExecuteNonQuerySqlInContext(dbContext);

                if (this.DataAction != null)
                    this.DataAction(this.Model);
            }

            return this.Model;
        }

        protected virtual void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteNonQuerySqlInContext();
        }
    }
}
