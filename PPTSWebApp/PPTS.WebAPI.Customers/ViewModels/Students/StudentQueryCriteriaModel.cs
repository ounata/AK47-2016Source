using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    [Serializable]
    public class StudentQueryCriteriaModel
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [InConditionMapping("CampusID")]
        public string[] OrgIds { get; set; }
        /// <summary>
        /// 当前年级
        /// </summary>
        [InConditionMapping("Grade")]
        public string[] Grade { get; set; }
        /// <summary>
        /// 建档时间起
        /// </summary>
        [ConditionMapping("CreateTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime CreateTimeStart { get; set; }
        /// <summary>
        /// 建档时间止
        /// </summary>
        [ConditionMapping("CreateTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime CreateTimeEnd { get; set; }
        /// <summary>
        /// 首次签约时间起
        /// </summary>
        [ConditionMapping("FirstSignTime", UtcTimeToLocal = true, Operation = ">=")]
        public DateTime FirstSignTimeStart { get; set; }
        /// <summary>
        /// 首次签约时间止
        /// </summary>
        [ConditionMapping("FirstSignTime", UtcTimeToLocal = true, Operation = "<", AdjustDays = 1)]
        public DateTime FirstSignTimeEnd { get; set; }
        /// <summary>
        /// 账户价值起
        /// </summary>
        [ConditionMapping("(AssetOneToOneMoney+AssetClassMoney+AssetOtherMoney+AccountMoney)", IsExpression = true, Operation = ">=")]
        public decimal AccountAmountStart { get; set; }
        /// <summary>
        /// 账户价值止
        /// </summary>
        [ConditionMapping("(AssetOneToOneMoney+AssetClassMoney+AssetOtherMoney+AccountMoney)", IsExpression = true, Operation = "<=")]
        public decimal AccountAmountEnd { get; set; }
        /// <summary>
        /// 剩余课时起
        /// </summary>
        [ConditionMapping("(AssetOneToOneAmount+AssetClassAmount+AssetOtherAmount)", IsExpression = true, Operation = ">=")]
        public decimal AssetAmountStart { get; set; }
        /// <summary>
        /// 剩余课时止
        /// </summary>
        [ConditionMapping("(AssetOneToOneAmount+AssetClassAmount+AssetOtherAmount)", IsExpression = true, Operation = "<=")]
        public decimal AssetAmountEnd { get; set; }
        /// <summary>
        /// 可用金额起
        /// </summary>
        [ConditionMapping("AccountMoney", IsExpression = true, Operation = ">=")]
        public decimal AvaiableAmountStart { get; set; }
        /// <summary>
        /// 可用金额止
        /// </summary>
        [ConditionMapping("AccountMoney", IsExpression = true, Operation = "<=")]
        public decimal AvaiableAmountEnd { get; set; }
        /// <summary>
        /// 信息来源-一级
        /// </summary>
        [InConditionMapping("SourceMainType")]
        public string[] SourceMainType { get; set; }
        /// <summary>
        /// 信息来源-二级
        /// </summary>
        [InConditionMapping("SourceSubType")]
        public string[] SourceSubType { get; set; }
        /// <summary>
        /// 客户等级
        /// </summary>
        [InConditionMapping("VipLevel")]
        public int[] VipLevels { get; set; }
        /// <summary>
        /// 有过转介绍的学员
        /// </summary>
        [ConditionMapping("ReferralCount", IsExpression = true, Operation = ">=")]
        public int ReferralCountStart { get; set; }
        /// <summary>
        /// 有过转介绍的学员
        /// </summary>
        [ConditionMapping("ReferralCount", IsExpression = true, Operation = ">=")]
        public int ReferralCountEnd { get; set; }
        /// <summary>
        /// 高三毕业库学员
        /// </summary>
        [NoMapping]
        public string GraduatedParam { get; set; }
        /// <summary>
        /// 高三毕业库学员
        /// </summary>
        [NoMapping]
        public string Graduated { get { return GraduatedParam == "-1" ? "" : GraduatedParam; } set { } }
        /// <summary>
        /// 学员或者家长姓名
        /// </summary>
        [NoMapping]
        public string KeyWord { get; set; }

        #region 学员类型

        /// <summary>
        /// 学员类型
        /// </summary>
        [NoMapping]
        public int CustomerType { get; set; }

        /// <summary>
        /// 有效学员-二级
        /// </summary>
        [NoMapping]
        public int ValidType { get; set; }

        /// <summary>
        /// 上课学员-二级
        /// </summary>
        [NoMapping]
        public int AttendType { get; set; }

        /// <summary>
        /// 停课学员-二级
        /// </summary>
        [NoMapping]
        public int StopType { get; set; }

        /// <summary>
        /// 休学学员-二级
        /// </summary>
        [NoMapping]
        public int SuspendType { get; set; }

        /// <summary>
        /// 结课学员-二级
        /// </summary>
        [NoMapping]
        public int CompletedType { get; set; }

        /// <summary>
        /// 学员类型开始时间
        /// </summary>
        [NoMapping]
        public DateTime StatusStartTime { get; set; }

        /// <summary>
        /// 学员类型开始时间
        /// </summary>
        [NoMapping]
        public DateTime StatusStartTimeUTC { get {return TimeZoneContext.Current.ConvertTimeToUtc(StatusStartTime); } set { } }

        /// <summary>
        /// 学员类型结束时间
        /// </summary>
        [NoMapping]
        public DateTime StatusEndTime { get; set; }

        /// <summary>
        /// 学员类型开始时间
        /// </summary>
        [NoMapping]
        public DateTime StatusEndTimeUTC { get { return TimeZoneContext.Current.ConvertTimeToUtc(StatusEndTime); } set { } }

        #endregion

        /// <summary>
        /// 据最后上课时间查询方式
        /// </summary>
        [NoMapping]
        public int LastCourseType { get; set; }

        /// <summary>
        /// 在读学校
        /// </summary>
        [ConditionMapping("CustomerSchoolName", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string SchoolName { get; set; }

        /// <summary>
        /// 归属关系
        /// </summary>
        [NoMapping]
        public int[] Belongs { get; set; }

        /// <summary>
        /// 归属人姓名
        /// </summary>
        [NoMapping]
        public string BelongName { get; set; }

        /// <summary>
        /// 建档关系
        /// </summary>
        [InConditionMapping("CreatorJobType")]
        public int[] Creation { get; set; }

        /// <summary>
        /// 建档人姓名
        /// </summary>
        [ConditionMapping("CreatorName")]
        public string CreatorName { get; set; }

        /// <summary>
        /// 建档部门
        /// </summary>
        [NoMapping]
        public string Dept { get; set; }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get;
            set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get;
            set;
        }
    }

    public class StudentQueryResultModel
    {
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string CampusID { get; set; }
        [DataMember]
        public string ParentName { get; set; }
        [DataMember]
        public string FirstSignTime { get; set; }
        [DataMember]
        public string SchoolName { get; set; }
        [DataMember]
        public string CustomerSchoolName { get; set; }
        [DataMember]
        public string Grade { get; set; }
        [DataMember]
        public string EducatorName { get; set; }
        [DataMember]
        public string ConsultantName { get; set; }
        [DataMember]
        public string AssignedAmount { get; set; }
        [DataMember]
        public string AssetRemainAmount { get; set; }
        [DataMember]
        public string AccountMoney { get; set; }

        [DataMember]
        public string OrderMoney { get; set; }

        [DataMember]
        public string AvaiableMoney { get; set; }

        [DataMember]
        public string LastAssignDays { get; set; }
    }

    [Serializable]
    public class StudentQueryResultModelCollection : EditableDataObjectCollectionBase<StudentQueryResultModel>
    {
    }

    public class StudentQueryResult
    {
        public PagedQueryResult<StudentQueryResultModel, StudentQueryResultModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}
