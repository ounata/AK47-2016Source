using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Adapters;

namespace PPTS.Data.Customers.Executors
{
    public class AccountDebitExecutor : PPTSCustomerExecutorBase
    {
        public AccountDebitExecutor(string opType) : base(opType)
        {

        }


        public string AccountID { set; get; }
        public decimal Money { set; get; }

        public Account Account { set; get; }

        public AccountRecord Record { set; get; }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            if(OperationType== "Debit")
            {
                AccountAdapter.Instance.UpdateInContext(Account);
                AccountRecordAdapter.Instance.UpdateInContext(Record);
                ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteTimePointSqlInContext());
            }else if (OperationType == "RollbackDebit")
            {
                AccountAdapter.Instance.UpdateInContext(Account);
                AccountRecordAdapter.Instance.DeleteInContext(Record);
                ConnectionDefine.GetDbContext().DoAction(db => db.ExecuteTimePointSqlInContext());
            }


            return null;
        }

        protected override string GetOperationDescription()
        {
            string description = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return OperationType;
            }
            return description;
        }

    }
}
