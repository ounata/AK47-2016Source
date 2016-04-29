using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Products.Adapters;
using System;

namespace PPTS.Data.Common.Executors
{
    /// <summary>
    /// 编辑实体用到的Executor的基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class PPTSEditProductExecutorBase<TModel> : PPTSEditEntityExecutorBase<TModel>
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public PPTSEditProductExecutorBase(TModel model, Action<TModel> dataAction) :
            base(model, dataAction)
        {
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        /// <param name="opType"></param>
        public PPTSEditProductExecutorBase(TModel model, Action<TModel> dataAction, string opType) :
            base(model, dataAction, opType)
        {
        }

        protected override void PersistOperationLogInContext(DataExecutionContext<UserOperationLogCollection> context)
        {
            context.Logs.ForEach(log => ProductUserOperationLogAdapter.Instance.InsertDataInContext(log));
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            using (DbContext dbContext = PPTS.Data.Products.ConnectionDefine.GetDbContext())
            {
                dbContext.ExecuteNonQuerySqlInContext();

                if (this.DataAction != null)
                    this.DataAction(this.Model);
            }

            return this.Model;
        }
    }
}
