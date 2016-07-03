﻿using MCS.Library.Data.Adapters;
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
    /// 退费
    /// </summary>
    public class CustomerSearchExecutor_ByAccountRefund : CustomerSearchExecutorBase
    {
        public override CustomerSearchUpdateType SearchUpdateType
        {
            get
            {
                return CustomerSearchUpdateType.AccountRefundApply;
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
            SelectSqlClauseBuilder selectBuilder = new SelectSqlClauseBuilder();
            selectBuilder.AppendFields(
                  "CustomerID"
                , "VerifyTime"
                , "ApplyRefundMoney"
                , "ThisAccountMoney"
                , "ThisAccountValue"
                , "ThisDiscountRate"
                , "ThisDiscountValue");

            string sql = string.Format(@"select {0} from {1} where VerifyStatus='{2} and {3}"
            , selectBuilder.ToSqlString(TSqlBuilder.Instance)
            , AccountRefundApplyAdapter.Instance.GetQueryMappingInfo().GetQueryTableName()
            , (int)RefundVerifyStatus.Refunded
            , inBuilder.ToSqlString(TSqlBuilder.Instance));
            return sql;
        }
    }
}