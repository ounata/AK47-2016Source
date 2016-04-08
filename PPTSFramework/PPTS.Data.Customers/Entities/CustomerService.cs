using MCS.Library.Core;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PPTS.Data.Customers.Entities
{
	/// <summary>
	/// This object represents the properties and methods of a CustomerService.
	/// 客服信息表
	/// </summary>
	[Serializable]
    [ORTableMapping("CustomerServices")]
    [DataContract]
	public class CustomerService
	{		
		public CustomerService()
		{
		}		

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
		/// 服务ID
		/// </summary>
		[ORFieldMapping("ServiceID", PrimaryKey=true)]
        [DataMember]
		public string ServiceID
		{
			get;
            set;
		}

		/// <summary>
		/// 服务类型（投诉，退费，咨询，其它）
		/// </summary>
		[ORFieldMapping("ServiceType")]
        [DataMember]
		public string ServiceType
		{
			get;
            set;
		}

		/// <summary>
		/// 服务状态
		/// </summary>
		[ORFieldMapping("ServiceStatus")]
        [DataMember]
		public string ServiceStatus
		{
			get;
            set;
		}

		/// <summary>
		/// 备注
		/// </summary>
		[ORFieldMapping("ServiceMemo")]
        [DataMember]
		public string ServiceMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 受理时间
		/// </summary>
		[ORFieldMapping("AcceptTime")]
        [DataMember]
		public DateTime AcceptTime
		{
			get;
            set;
		}

		/// <summary>
		/// 要求分客服受理并回复时间限制代码（2小时内，6小时内，12小时内，24小时内）
		/// </summary>
		[ORFieldMapping("AcceptLimit")]
        [DataMember]
		public string AcceptLimit
		{
			get;
            set;
		}

		/// <summary>
		/// 分客服受理并回复时间限制值
		/// </summary>
		[ORFieldMapping("AcceptLimitValue")]
        [DataMember]
		public decimal AcceptLimitValue
		{
			get;
            set;
		}

		/// <summary>
		/// 受理描述
		/// </summary>
		[ORFieldMapping("AcceptMemo")]
        [DataMember]
		public string AcceptMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 受理人ID
		/// </summary>
		[ORFieldMapping("AccepterID")]
        [DataMember]
		public string AccepterID
		{
			get;
            set;
		}

		/// <summary>
		/// 受理人姓名
		/// </summary>
		[ORFieldMapping("AccepterName")]
        [DataMember]
		public string AccepterName
		{
			get;
            set;
		}

		/// <summary>
		/// 受理人岗位ID
		/// </summary>
		[ORFieldMapping("AccepterJobID")]
        [DataMember]
		public string AccepterJobID
		{
			get;
            set;
		}

		/// <summary>
		/// 受理人岗位名称
		/// </summary>
		[ORFieldMapping("AccepterJobName")]
        [DataMember]
		public string AccepterJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 客户诉求
		/// </summary>
		[ORFieldMapping("AppealMemo")]
        [DataMember]
		public string AppealMemo
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
		/// 咨询师岗位名称
		/// </summary>
		[ORFieldMapping("ConsultantJobName")]
        [DataMember]
		public string ConsultantJobName
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
		/// 学管师岗位名称
		/// </summary>
		[ORFieldMapping("EducatorJobName")]
        [DataMember]
		public string EducatorJobName
		{
			get;
            set;
		}

		/// <summary>
		/// 咨询类型代码（校区相关，退费相关，加盟，其它）
		/// </summary>
		[ORFieldMapping("ConsultType")]
        [DataMember]
		public string ConsultType
		{
			get;
            set;
		}

		/// <summary>
		/// 如果咨询类型是其它，则录入该内容
		/// </summary>
		[ORFieldMapping("ConsultMemo")]
        [DataMember]
		public string ConsultMemo
		{
			get;
            set;
		}

		/// <summary>
		/// 投诉次数代码（一次，二次，三次，三次以上...)
		/// </summary>
		[ORFieldMapping("ComplaintTimes")]
        [DataMember]
		public string ComplaintTimes
		{
			get;
            set;
		}

		/// <summary>
		/// 严重程度代码（普通，严重，紧急）
		/// </summary>
		[ORFieldMapping("ComplaintLevel")]
        [DataMember]
		public string ComplaintLevel
		{
			get;
            set;
		}

		/// <summary>
		/// 投诉升级代码（二级，三级，特级）
		/// </summary>
		[ORFieldMapping("ComplaintUpgrade")]
        [DataMember]
		public string ComplaintUpgrade
		{
			get;
            set;
		}

		/// <summary>
		/// 是否升级处理
		/// </summary>
		[ORFieldMapping("IsUpgradeHandle")]
        [DataMember]
		public int IsUpgradeHandle
		{
			get;
            set;
		}

		/// <summary>
		/// 下一处理岗类型（分客服专员，分客服经理，总客服经理...）
		/// </summary>
		[ORFieldMapping("HandleJobType")]
        [DataMember]
		public string HandleJobType
		{
			get;
            set;
		}

		/// <summary>
		/// 是否发送邮件
		/// </summary>
		[ORFieldMapping("IsSendEmail")]
        [DataMember]
		public int IsSendEmail
		{
			get;
            set;
		}

		/// <summary>
		/// 处理人邮箱
		/// </summary>
		[ORFieldMapping("HandlerEmail")]
        [DataMember]
		public string HandlerEmail
		{
			get;
            set;
		}

		/// <summary>
		/// 是否发送短信
		/// </summary>
		[ORFieldMapping("IsSendMessage")]
        [DataMember]
		public int IsSendMessage
		{
			get;
            set;
		}

		/// <summary>
		/// 处理人手机
		/// </summary>
		[ORFieldMapping("HandlerPhone")]
        [DataMember]
		public string HandlerPhone
		{
			get;
            set;
		}

		/// <summary>
		/// 语音文件ID
		/// </summary>
		[ORFieldMapping("VoiceID")]
        [DataMember]
		public string VoiceID
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
		[ORFieldMapping("CreateTime")]
        [DataMember]
		public DateTime CreateTime
		{
			get;
            set;
		}

		/// <summary>
		/// 最后修改人ID
		/// </summary>
		[ORFieldMapping("ModifierID")]
        [DataMember]
		public string ModifierID
		{
			get;
            set;
		}

		/// <summary>
		/// 最后修改人姓名
		/// </summary>
		[ORFieldMapping("ModifierName")]
        [DataMember]
		public string ModifierName
		{
			get;
            set;
		}

		/// <summary>
		/// 最后修改时间
		/// </summary>
		[ORFieldMapping("ModifyTime")]
        [DataMember]
		public DateTime ModifyTime
		{
			get;
            set;
		}
	}

    [Serializable]
    [DataContract]
    public class CustomerServiceCollection : EditableDataObjectCollectionBase<CustomerService>
    {
    }
}