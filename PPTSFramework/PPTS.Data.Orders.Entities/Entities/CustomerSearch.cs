using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using PPTS.Data.Common.Entities;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;

namespace PPTS.Data.Orders.Entities
{
    [Serializable]
    [ORTableMapping("SM.CustomerSearch")]
    [DataContract]
    public class CustomerSearch
    {
        public CustomerSearch()
        { }

        /// <summary>
		/// 学员ID
		/// </summary>
		[ORFieldMapping("CustomerID", PrimaryKey = true)]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }     
       
        /// <summary>
        /// 性别代码
        /// </summary>
        [ORFieldMapping("Gender")]
        [ConstantCategory("C_CODE_ABBR_GENDER")]
        [DataMember]
        public string Gender
        {
            get;set;
        }
        /// <summary>
        /// 出生年月
        /// </summary>
        [ORFieldMapping("Birthday")]
        [DataMember]
        public DateTime Birthday
        {
            get;set;
        }
        /// <summary>
        /// 在读学校名称
        /// </summary>
        [ORFieldMapping("SchoolName")]
        [DataMember]
        public string SchoolName
        {
            get;set;
        }
        /// <summary>
        /// 当前年级代码
        /// </summary>
        [ORFieldMapping("Grade")]
        [ConstantCategory("C_CODE_ABBR_CUSTOMER_GRADE")]
        [DataMember]
        public string Grade
        {
            get; set;
        }
        /// <summary>
        /// 学管师/班主任
        /// </summary>
        [ORFieldMapping("EducatorName")]
        [DataMember]
        public string EducatorName
        {
            get;set;
        }
        /// <summary>
        /// 一对一剩余课次数
        /// </summary>
        [ORFieldMapping("AssetOneToOneAmount")]
        [DataMember]
        public decimal AssetOneToOneAmount
        {
            get;
            set;
        }
    }


    [Serializable]
    [DataContract]
    public class CustomerSearchCollection : EditableDataObjectCollectionBase<CustomerSearch>
    {

    }
}
