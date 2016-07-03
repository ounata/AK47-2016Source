using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.UnionPay.Executors;
using System;
using System.Collections.Generic;

namespace PPTS.ExtServices.UnionPay.Models.Statement
{
    [Serializable]
    public class POSRecordsModel
    {
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime
        {
            get;
            set;
        }

        /// <summary>
        /// 交易类型 
        /// 1--银联   4--通联
        /// </summary>
        public string TransactionType
        {
            get;
            set;
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent
        {
            get;
            set;
        }

        /// <summary>
        /// POS刷卡记录集合
        /// </summary>
        public POSRecordCollection POSRecordCollection
        {
            get;
            set;
        }

        public void UpdatePOSRecord()
        {
            AddPosRecordsExecutor executor = new AddPosRecordsExecutor(this);
            executor.Execute();
        }
    }
}