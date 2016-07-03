using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common;

namespace PPTS.Data.Orders.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a AssetConfirm.
	/// 资产收入确认记录表
	/// </summary>
	[Serializable]
    [ORTableMapping("OM.AssetConfirms")]
    [DataContract]
	public class AssetConfirm : IEntityWithCreator
    {
        /// <summary>
        /// 校区ID
        /// </summary>
        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID
        {
            get;
            set;
        }
        /// <summary>
        /// 校区名称
        /// </summary>
        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName
        {
            get;
            set;
        }

   

        /// <summary>
        /// 学员ID
        /// </summary>
        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID
        {
            get;
            set;
        }

        /// <summary>
        /// 学员编码
        /// </summary>
        [ORFieldMapping("CustomerCode")]
        [DataMember]
        public string CustomerCode
        {
            get;
            set;
        }

        /// <summary>
        /// 学员姓名
        /// </summary>
        [ORFieldMapping("CustomerName")]
        [DataMember]
        public string CustomerName
        {
            get;
            set;
        }

        /// <summary>
        /// 账户ID
        /// </summary>
        [ORFieldMapping("AccountID")]
        [DataMember]
        public string AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产ID
        /// </summary>
        [ORFieldMapping("AssetID")]
        [DataMember]
        public string AssetID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产编码
        /// </summary>
        [ORFieldMapping("AssetCode")]
        [DataMember]
        public string AssetCode
        {
            get;
            set;
        }

        /// <summary>
        /// 资产类型
        /// </summary>
        [ORFieldMapping("AssetType")]
        [DataMember]
        public AssetTypeDefine AssetType
        {
            get;
            set;
        }

        /// <summary>
        /// 资产来源类型
        /// </summary>
        [ORFieldMapping("AssetRefType")]
        [DataMember]
        public AssetRefTypeDefine AssetRefType
        {
            get;
            set;
        }

        /// <summary>
        /// 资产来源PID（存放订购单ID）
        /// </summary>
        [ORFieldMapping("AssetRefPID")]
        [DataMember]
        public string AssetRefPID
        {
            get;
            set;
        }

        /// <summary>
        /// 资产来源ID（存放订购明细ID）
        /// </summary>
        [ORFieldMapping("AssetRefID")]
        [DataMember]
        public string AssetRefID
        {
            get;
            set;
        }

        /// <summary>
        /// 上次资产剩余价值
        /// </summary>
        [ORFieldMapping("AssetMoney")]
        [DataMember]
        public decimal AssetMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 确认单ID
        /// </summary>
        [ORFieldMapping("ConfirmID", PrimaryKey = true)]
        [DataMember]
        public string ConfirmID
        {
            get;
            set;
        }

        /// <summary>
        /// 确认标志（1-收入确认，-1收入取消）
        /// </summary>
        [ORFieldMapping("ConfirmFlag")]
        [DataMember]
        public ConfirmFlagDefine ConfirmFlag
        {
            get;
            set;
        }

        /// <summary>
        /// 确认金额
        /// </summary>
        [ORFieldMapping("ConfirmMoney")]
        [DataMember]
        public decimal ConfirmMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 确认说明
        /// </summary>
        [ORFieldMapping("ConfirmMemo")]
        [DataMember]
        public string ConfirmMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 确认状态（1-已确认，3-已删除 ）参考排课
        /// </summary>
        [ORFieldMapping("ConfirmStatus")]
        [DataMember]
        public ConfirmStatusDefine ConfirmStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 确认时间
        /// </summary>
        [ORFieldMapping("ConfirmTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime ConfirmTime
        {
            get;
            set;
        }

        /// <summary>
        /// 确认人ID
        /// </summary>
        [ORFieldMapping("ConfirmerID")]
        [DataMember]
        public string ConfirmerID
        {
            get;
            set;
        }

        /// <summary>
        /// 确认人姓名
        /// </summary>
        [ORFieldMapping("ConfirmerName")]
        [DataMember]
        public string ConfirmerName
        {
            get;
            set;
        }

        /// <summary>
        /// 确认人岗位ID
        /// </summary>
        [ORFieldMapping("ConfirmerJobID")]
        [DataMember]
        public string ConfirmerJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 确认人岗位名称
        /// </summary>
        [ORFieldMapping("ConfirmerJobName")]
        [DataMember]
        public string ConfirmerJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 确认人岗位类型代码
        /// </summary>
        [ORFieldMapping("ConfirmerJobType")]
        [DataMember]
        public string ConfirmerJobType
        {
            get;
            set;
        }

        /// <summary>
        /// 异步处理状态（参考订购）
        /// </summary>
        [ORFieldMapping("ProcessStatus")]
        [DataMember]
        public ProcessStatusDefine ProcessStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 异步处理时间
        /// </summary>
        [ORFieldMapping("ProcessTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime ProcessTime
        {
            get;
            set;
        }

        /// <summary>
        /// 异步处理说明
        /// </summary>
        [ORFieldMapping("ProcessMemo")]
        [DataMember]
        public string ProcessMemo
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师ID
        /// </summary>
        [ORFieldMapping("ConsultantID")]
        [DataMember]
        public string ConsultantID
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师姓名
        /// </summary>
        [ORFieldMapping("ConsultantName")]
        [DataMember]
        public string ConsultantName
        {
            get;
            set;
        }

        /// <summary>
        /// 咨询师岗位ID
        /// </summary>
        [ORFieldMapping("ConsultantJobID")]
        [DataMember]
        public string ConsultantJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师ID
        /// </summary>
        [ORFieldMapping("EducatorID")]
        [DataMember]
        public string EducatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师姓名
        /// </summary>
        [ORFieldMapping("EducatorName")]
        [DataMember]
        public string EducatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 学管师岗位ID
        /// </summary>
        [ORFieldMapping("EducatorJobID")]
        [DataMember]
        public string EducatorJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师ID
        /// </summary>
        [ORFieldMapping("TeacherID")]
        [DataMember]
        public string TeacherID
        {
            get;
            set;
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [ORFieldMapping("TeacherName")]
        [DataMember]
        public string TeacherName
        {
            get;
            set;
        }

        /// <summary>
        /// 教师岗位ID
        /// </summary>
        [ORFieldMapping("TeacherJobID")]
        [DataMember]
        public string TeacherJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 上课开始时间
        /// </summary>
        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 上课结束时间
        /// </summary>
        [ORFieldMapping("EndTime", UtcTimeToLocal = true)]
        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 课次时长
        /// </summary>
        [ORFieldMapping("DurationValue")]
        [DataMember]
        public decimal DurationValue
        {
            get;
            set;
        }

        /// <summary>
        /// 课次数
        /// </summary>
        [ORFieldMapping("Amount")]
        [DataMember]
        public decimal Amount
        {
            get;
            set;
        }

        /// <summary>
        /// 课时单价
        /// </summary>
        [ORFieldMapping("Price")]
        [DataMember]
        public decimal Price
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [ORFieldMapping("CreatorID")]
        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [ORFieldMapping("CreatorName")]
        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where, DefaultExpression = "GETUTCDATE()")]
        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AssetConfirmCollection : EditableDataObjectCollectionBase<AssetConfirm>
    {
    }
}