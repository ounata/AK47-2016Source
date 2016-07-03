using MCS.Library.Net.SNTP;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common
{
    /// <summary>
    /// 互斥处理的帮助类
    /// </summary>
    public class MutexHelper
    {
    }

    public class MutexLocker
    {
        MutexLockParameter[] _p;
        MutexReleaser _releaser;
        public MutexLocker(params MutexLockParameter[] p)
        {
            if (p == null || p.Length == 0)
                throw new ArgumentNullException("p");

            _p = p;
            if (p.Length != 0)
            {
                _releaser = new MutexReleaser(new MutexReleaseParameter(p[0]));
            }
        }

        private void Lock()
        {
            try
            {
                this.InnerLock();
            }
            catch
            {
                try
                {
                    this.InnerLock();
                }
                catch
                {
                    this.InnerLock();
                }
            }
        }
        private void InnerLock()
        {
            List<MutexRecord> list = MutexLocker.GetMutexRecords(_p);
            if (list.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (MutexRecord record in list)
                    sb.AppendLine(record.Description);
                throw new Exception(sb.ToString());
            }
            new MutexRecordSaveExecutor(_p).Execute();
        }
        internal static List<MutexRecord> GetMutexRecords(MutexLockParameter[] ps)
        {
            List<string> keys = new List<string>();
            foreach (MutexLockParameter p in ps)
                keys.AddRange(p.QueryMutexKeys);
            MutexRecordCollection records = MutexRecordAdapter.Instance.Load(keys.ToArray());

            List<MutexRecord> list = new List<MutexRecord>();
            foreach (MutexLockParameter p in ps)
            {
                p.MutexActions = MutexSettingsCache.GetMutexActions(p.Action);
                foreach (string key in p.QueryMutexKeys)
                {
                    foreach (MutexRecord record in records.Where(x => x.MutexKey == key))
                    {
                        //不是当前单据而且包含在互斥规则里并且没有过期
                        if (record.BizBillID != p.BillID && p.MutexActions.Contains(record.BizAction) && record.ExpireTime > SNTPClient.AdjustedTime)
                            list.Add(record);
                    }
                }
            }
            return list;
        }

        public void Lock(Action action)
        {
            this.Lock();
            if (action != null)
            {
                try
                {
                    action();
                }
                catch
                {
                    _releaser.Release();
                    throw;
                }
            }
        }

        public void Lock(Action action1, Action action2)
        {
            this.Lock();
            if (action1 != null)
            {
                try
                {
                    action1();
                }
                catch
                {
                    _releaser.Release();
                    throw;
                }
            }
            if (action2 != null)
            {
                try
                {
                    action2();
                }
                catch
                {
                    _releaser.Release();
                    throw;
                }
            }
        }

        public void LockAndRelease(Action action)
        {
            foreach (MutexLockParameter p in _p)
            {
                p.Timeout = 3 * 60 * 1000;
            }
            this.Lock();
            if (action != null)
            {
                try
                {
                    action();
                }
                finally
                {
                    _releaser.Release();
                }
            }
        }
    }

    public class MutexReleaser
    {
        MutexReleaseParameter _p;
        public MutexReleaser(MutexReleaseParameter p)
        {
            if (p == null)
                throw new ArgumentNullException("p");
            _p = p;
        }

        internal void Release()
        {
            try
            {
                this.InnerRelease();
            }
            catch
            {
                try
                {
                    this.InnerRelease();
                }
                catch
                {
                    this.InnerRelease();
                }
            }
        }
        private void InnerRelease()
        {
            MutexRecordAdapter.Instance.Delete(_p.BillID);
        }
        public void Release(Action action)
        {
            if (action != null)
            {
                action();
            }
            this.Release();
        }
    }

    /// <summary>
    /// 互斥参数
    /// </summary>
    public class MutexLockParameter
    {
        /// <summary>
        /// 学员ID
        /// </summary>
        public string CustomerID
        {
            set;
            get;
        }
        
        /// <summary>
        /// 账户ID
        /// </summary>
        public string AccountID
        {
            set;
            get;
        }
        
        /// <summary>
        /// 账户ID列表
        /// </summary>
        public List<string> AccountIDs
        {
            set;
            get;
        }

        /// <summary>
        /// 操作
        /// </summary>
        public MutexAction Action
        {
            set;
            get;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set;
            get;
        }

        /// <summary>
        /// 超时毫秒秒数
        /// </summary>
        public double Timeout
        {
            set;
            get;
        }

        /// <summary>
        /// 单ID
        /// </summary>
        public string BillID
        {
            set;
            get;
        }
        
        public string MutexKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.AccountID))
                    return "C_" + this.CustomerID;
                else
                    return "A_" + this.AccountID;
            }
        }

        public string[] QueryMutexKeys
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("C_" + this.CustomerID);
                if (!string.IsNullOrEmpty(this.AccountID))
                    list.Add("A_" + this.AccountID);
                if (this.AccountIDs != null)
                {
                    foreach (string accountID in this.AccountIDs)
                        list.Add("A_" + accountID);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime
        {
            get
            {
                if (this.Timeout > 0)
                    return SNTPClient.AdjustedTime.AddMilliseconds(this.Timeout);
                else
                    return DateTime.MaxValue;
            }
        }

        /// <summary>
        /// 互斥操作列表
        /// </summary>
        public List<MutexAction> MutexActions
        {
            set;
            get;
        }
    }

    public class MutexReleaseParameter
    {
        public MutexReleaseParameter()
        {

        }
        internal MutexReleaseParameter(MutexLockParameter p)
        {
            this.BillID = p.BillID;
        }

        /// <summary>
        /// 单ID
        /// </summary>
        public string BillID
        {
            set;
            get;
        }
    }
}
