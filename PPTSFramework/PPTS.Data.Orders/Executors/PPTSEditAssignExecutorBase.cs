using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Common.Executors;
using PPTS.Data.Orders.Adapters;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;

namespace PPTS.Data.Orders.Executors
{
    public abstract class PPTSEditAssignExecutorBase<TModel> : PPTSEditEntityExecutorBase<TModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public PPTSEditAssignExecutorBase(TModel model, Action<TModel> dataAction) :
            base(model, dataAction)
        {

        }





        protected override void PersistOperationLogInContext(DataExecutionContext<UserOperationLogCollection> context)
        {
            DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Campus);

            context.Logs.ForEach(log => AssignsOperationLogAdapter.Instance.InsertDataInContext(log));
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            using (DbContext dbContext = PPTS.Data.Orders.ConnectionDefine.GetDbContext())
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
