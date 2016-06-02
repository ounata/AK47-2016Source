using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.UnionPay.Executors;

namespace PPTS.ExtServices.UnionPay.Models.Statement
{
    public class POSRecordsModel
    {
        /// <summary>
        /// 银联刷卡记录
        /// </summary>
        public POSRecord PosRecord
        { get; set; }

        public void UpdatePOSRecord()
        {
            AddPosRecordsExecutor executor = new AddPosRecordsExecutor(this);
            executor.Execute();
        }
    }
}