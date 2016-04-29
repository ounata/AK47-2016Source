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
        /// <summary>
        /// 市场专员
        /// </summary>
        [EnumItemDescription("市场专员")]
        Market,

        /// <summary>
        /// 坐席专员
        /// </summary>
        [EnumItemDescription("坐席专员")]
        Callcenter,

        /// <summary>
        /// 咨询师
        /// </summary>
        [EnumItemDescription("咨询师")]
        Consultant,

        /// <summary>
        /// 学管师
        /// </summary>
        [EnumItemDescription("学管师")]
        Educator,

        /// <summary>
        /// 教师
        /// </summary>
        [EnumItemDescription("教师")]
        Teacher
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
        Campus = DepartmentType.Campus
    }
}
