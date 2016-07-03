using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 账户日志模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class AccountRecordQueryModel : AccountRecord
    {
        /// <summary>
        /// 收入
        /// </summary>
        [DataMember]
        public decimal IncomeMoney
        {
            get
            {
                switch(this.RecordType)
                {
                    case AccountRecordType.Charge:
                    case AccountRecordType.Return:
                    case AccountRecordType.TransferIn:
                        return this.BillMoney;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// 支出
        /// </summary>
        [DataMember]
        public decimal ExpendMoney
        {
            get
            {
                switch (this.RecordType)
                {
                    case AccountRecordType.Refund:
                    case AccountRecordType.Deduct:
                    case AccountRecordType.TransferOut:
                        return this.BillMoney;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// 冻结
        /// </summary>
        [DataMember]
        public decimal FrozeMoney
        {
            get
            {
                switch (this.RecordType)
                {
                    case AccountRecordType.Order:
                        return this.BillMoney;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// 解冻
        /// </summary>
        [DataMember]
        public decimal ReleaseMoney
        {
            get
            {
                switch (this.RecordType)
                {
                    case AccountRecordType.Debook:
                        return this.BillMoney;
                    default:
                        return 0;
                }
            }
        }
    }

    [Serializable]
    [DataContract]
    public class AccountRecordQueryModelCollection : EditableDataObjectCollectionBase<AccountRecordQueryModel>
    {

    }
}