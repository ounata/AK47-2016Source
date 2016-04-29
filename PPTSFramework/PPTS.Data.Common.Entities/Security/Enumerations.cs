using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Core;

namespace PPTS.Data.Common.Security
{
    public enum GroupType
    {
        /// <summary>
        /// 默认组
        /// </summary>
        Normal = 0,

        /// <summary>
        ///  岗位
        /// </summary>
        Job = 1,
    }

    /// <summary>
    /// 部门类型
    /// </summary>
    public enum DepartmentType
    {
        None = 0,

        [EnumItemDescription("总公司", Category = "DataScope")]
        HQ = 1,

        [EnumItemDescription("分公司", Category = "DataScope")]
        Branch = 2,

        [EnumItemDescription("校区", Category = "DataScope")]
        Campus = 3,

        [EnumItemDescription("部门")]
        Department = 4,

        [EnumItemDescription("学科组")]
        XueKeZu = 5,

        [EnumItemDescription("办公点")]
        BanGongDian = 6,

        [EnumItemDescription("学科教研室")]
        XueKeJiaoYanShi = 7,

        [EnumItemDescription("大区", Category = "DataScope")]
        Region = 8
    }
}
