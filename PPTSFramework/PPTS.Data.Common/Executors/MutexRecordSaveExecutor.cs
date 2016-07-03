using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;

namespace PPTS.Data.Common.Executors
{
    [DataExecutorDescription("保存互斥记录")]
    public class MutexRecordSaveExecutor : PPTSExecutorBase
    {
        /// <summary>
        /// 互斥记录
        /// </summary>
        private MutexLockParameter[] _ps;
        public MutexRecordSaveExecutor(MutexLockParameter[] ps)
            : base("MutexSave")
        {
            _ps = ps;
        }

        protected override string GetOperationDescription()
        {
            string description = base.GetOperationDescription();
            if (string.IsNullOrWhiteSpace(description))
            {
                return this.OperationType;
            }
            return description;
        }

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            foreach (MutexLockParameter p in _ps)
            {
                MutexRecord record = new MutexRecord();
                record.MutexKey = p.MutexKey;
                record.BizAction = p.Action;
                record.BizActionText = p.Action.ToString();
                record.ExpireTime = p.ExpireTime;
                record.BizBillID = p.BillID;
                record.Description = p.Description;

                MutexRecordAdapter.Instance.Update(record);
            }
            List<MutexRecord> list = MutexLocker.GetMutexRecords(_ps);
            if (list.Count != 0)
                throw new Exception("存在并发处理异常，请重试！");
            return true;
        }
    }
}