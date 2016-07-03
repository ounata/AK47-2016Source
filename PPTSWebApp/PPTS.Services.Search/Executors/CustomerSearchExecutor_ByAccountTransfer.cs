using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using PPTS.Data.Customers.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using PPTS.Contracts.Search.Models;
using PPTS.Data.Customers;

namespace PPTS.Services.Search.Executors
{
    /// <summary>
    /// 充值
    /// </summary>
    public class CustomerSearchExecutor_ByAccountTransfer : CustomerSearchExecutorBase
    {
        public override CustomerSearchUpdateType SearchUpdateType
        {
            get
            {
                return CustomerSearchUpdateType.AccountTransferApply;
            }
        }
        protected override DataTable PrepareData(IList<string> customerIDs)
        {
            string sql = this.SelectSql(customerIDs);
            DataSet ds = DbHelper.RunSqlReturnDS(sql, CustomerAdapter.Instance.GetDbContext().Name);

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
            InSqlClauseBuilder inBuilderBiz = new InSqlClauseBuilder("BizCustomerID");
            inBuilderBiz.AppendItem(customerIDs.ToArray());
            SelectSqlClauseBuilder selectBuilder = new SelectSqlClauseBuilder();
            selectBuilder.AppendFields(
                  "CustomerID"
                , "ApproveTime"
                , "TransferMoney"
                , "ThisAccountMoney"
                , "ThisAccountValue"
                , "ThisDiscountRate"
                , "ThisDiscountValue");

            string sql = string.Format(@"select {0} from {1} where ApplyStatus='{2} and {3} and {4}"
            , selectBuilder.ToSqlString(TSqlBuilder.Instance)
            , AccountTransferApplyAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , (int)ApplyStatusDefine.Approved
            , inBuilder.ToSqlString(TSqlBuilder.Instance)
            , inBuilderBiz.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}