using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Contracts.Search.Models;
using MCS.Library.Data.Adapters;
using PPTS.Data.Customers.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers;

namespace PPTS.Services.Search.Executors
{
    public class CustomerSearchExecutor_ByCustomerFollow : CustomerSearchExecutorBase
    {
        public override CustomerSearchUpdateType SearchUpdateType
        {
            get
            {
                return CustomerSearchUpdateType.CustomerFollow;
            }
        }

        protected override DataTable PrepareData(IList<string> customerIDs)
        {
            string sql = this.SelectSql(customerIDs);
            DataSet ds = DbHelper.RunSqlReturnDS(sql, CustomerFollowAdapter.Instance.GetDbContext().Name);

            //构建CustomerSearch的字段生成DataTable
            DataTable dt = this.CreateTable();
            //向构建的Table存放数据
            foreach (string customerID in customerIDs)
            {
                foreach (DataRow row in ds.Tables[0].Select("CustomerID='" + customerID + "'"))
                {

                }
            }
            return dt;
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            return dt;
        }

        private string SelectSql(IList<string> customerIDs)
        {
            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("CustomerID");
            inBuilder.AppendItem(customerIDs.ToArray());
            SelectSqlClauseBuilder selectBuilder = new SelectSqlClauseBuilder();
            selectBuilder.AppendFields(
                  "CustomerID"
                , "FollowTime"
                , "FollowStage"
                , "NextFollowTime");

            string sql = string.Format(@"select {0} from {1} where PayStatus='{2} and {3}"
            , selectBuilder.ToSqlString(TSqlBuilder.Instance)
            , AccountChargeApplyAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , (int)PayStatusDefine.Paid
            , inBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}
