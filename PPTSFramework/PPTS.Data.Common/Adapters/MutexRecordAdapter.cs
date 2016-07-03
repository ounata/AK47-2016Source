using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Common.Adapters
{
    public class MutexRecordAdapter : UpdatableAndLoadableAdapterBase<MutexRecord, MutexRecordCollection>
    {
        public static readonly MutexRecordAdapter Instance = new MutexRecordAdapter();

        private MutexRecordAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }

        public MutexRecordCollection Load(string[] mutexKeys)
        {
            string inStr = "";
            foreach (string mutexKey in mutexKeys)
                inStr += "\'" + mutexKey + "\',";
            inStr = "(" + inStr.TrimEnd(',') + ")";

            return this.Load(builder => builder.AppendItem("MutexKey", inStr, "in", true));
        }

        public void Delete(string billID)
        {
            this.Delete(builder => builder.AppendItem("BizBillID", billID));
        }
    }
}