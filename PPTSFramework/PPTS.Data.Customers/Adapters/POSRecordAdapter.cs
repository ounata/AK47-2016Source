using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Adapters
{
    public class POSRecordAdapter : CustomerAdapterBase<POSRecord, POSRecordCollection>
    {
        public static readonly POSRecordAdapter Instance = new POSRecordAdapter();

        private POSRecordAdapter()
        {
        }

        /// <summary>
        /// 查询记录
        /// </summary>
        /// <param name="merchantID">校区ID</param>
        /// <param name="transactionID">交易参考号（流水号）</param>
        /// <param name="transactionType">交易类型（1-银联，4-通联）</param>
        /// <returns></returns>
        public POSRecord Load(string merchantID, string transactionID, string transactionType)
        {
            //return this.Load(builder => builder.AppendItem("MerchantID", merchantID)
            //                                    .AppendItem("TransactionID", transactionID)
            //                                    .AppendItem("TransactionType", transactionType)).SingleOrDefault();

            return this.Load(builder => builder.AppendItem("TransactionID", transactionID)).SingleOrDefault();
        }
    }
}
