using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 账户复合信息
    /// </summary>
    [Serializable]
    public class AccountCombinationModel
    {
        /// <summary>
        /// 所在校区ID
        /// </summary>
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }

        /// <summary>
        /// 所在校区名称
        /// </summary>
        [DataMember]
        public string CampusName
        {
            get;
            set;
        }

        /// <summary>
        /// 学员ID
        /// </summary>
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员编码
        /// </summary>
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 学员名称
        /// </summary>
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 当前年级
        /// </summary>
        [DataMember]
        public string GradeName
        {
            get;
            set;
        }

        /// <summary>
        /// 账户列表
        /// </summary>
        [DataMember]
        public List<AccountModel> Items
        {
            get;
            set;
        }
    }
}