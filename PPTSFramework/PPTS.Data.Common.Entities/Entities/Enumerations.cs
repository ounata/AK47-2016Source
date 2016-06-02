using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;
using PPTS.Data.Common.Security;

namespace PPTS.Data.Common
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType
    {
        [EnumItemDescription("未知")]
        Unknown = 0,

        [EnumItemDescription("男")]
        Male = 1,

        [EnumItemDescription("女")]
        Female = 2
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IDTypeDefine
    {
        [EnumItemDescription("身份证")]
        IDCard = 1,

        [EnumItemDescription("军官证")]
        MilitaryOfficerCard = 2,

        [EnumItemDescription("驾驶证")]
        DrivingLicense = 3,

        [EnumItemDescription("护照")]
        Passport = 4
    }

    /// <summary>
    /// 打印状态
    /// </summary>
    public enum PrintStatusDefine
    {
        /// <summary>
        /// 未打印
        /// </summary>
        [EnumItemDescription("未打印")]
        UnPrint,

        /// <summary>
        /// 已打印
        /// </summary>
        [EnumItemDescription("已打印")]
        Printed
    }

    /// <summary>
    /// 岗位状态
    /// </summary>
    public enum JobStatusDefine
    {
        /// <summary>
        /// 在职
        /// </summary>
        [EnumItemDescription("在职")]
        Enabled,

        /// <summary>
        /// 离职
        /// </summary>
        [EnumItemDescription("离职")]
        Disabled
    }

    /// <summary>
    /// 岗位类型
    /// </summary>
    public enum JobTypeDefine
    {
        [EnumItemDescription("未知", Filter = "")]
        Unknown = 0,

        /// <summary>
        /// 咨询师
        /// </summary>
        [EnumItemDescription("咨询师", Filter = "校教育咨询师", Category = "SC.ConsultantJobSnapshot_Current")]
        Consultant = 1,

        /// <summary>
        /// 学管师
        /// </summary>
        [EnumItemDescription("学管师", Filter = "校学习管理师", Category = "SC.EducatorJobSnapshot_Current")]
        Educator = 2,

        /// <summary>
        /// 教师
        /// </summary>
        [EnumItemDescription("教师", Filter = "教师", Category = "TeacherJobSnapshot_Current")]
        Teacher = 3,

        /// <summary>
        /// 坐席专员
        /// </summary>
        [EnumItemDescription("坐席专员", Filter = "呼叫中心专员", Category = "SC.CallcenterJobSnapshot_Current")]
        Callcenter = 4,

        /// <summary>
        /// 市场专员
        /// </summary>
        [EnumItemDescription("市场专员", Filter = "市场专员", Category = "SC.MarketingJobSnapshot_Current")]
        Marketing = 5,

        /// <summary>
        /// 校区总监
        /// </summary>
        [EnumItemDescription("校区总监", Filter = "校区总监")]
        CampusDirector = 10
    }

    /// <summary>
    /// 组织机构类型
    /// </summary>
    public enum OrgTypeDefine
    {
        /// <summary>
        /// 大区
        /// </summary>
        Region = DepartmentType.Region,

        /// <summary>
        /// 分公司
        /// </summary>
        Branch = DepartmentType.Branch,

        /// <summary>
        /// 校区
        /// </summary>
        Campus = DepartmentType.Campus,

        /// <summary>
        /// 学科组
        /// </summary>
        XueKeZu = DepartmentType.XueKeZu
    }

    /// <summary>
    /// 处理状态
    /// </summary>
    public enum ProcessStatusDefine
    {
        /// <summary>
        /// 待处理
        /// </summary>
        [EnumItemDescription("待处理")]
        Waiting,

        /// <summary>
        /// 处理中
        /// </summary>
        [EnumItemDescription("处理中")]
        Processing,

        /// <summary>
        /// 处理完
        /// </summary>
        [EnumItemDescription("处理完")]
        Processed,

        /// <summary>
        /// 错误
        /// </summary>
        [EnumItemDescription("错误")]
        Error
    }

}
