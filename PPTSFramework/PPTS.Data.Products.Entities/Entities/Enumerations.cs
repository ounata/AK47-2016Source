using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Products
{
    /// <summary>
    /// 产品分类
    /// </summary>
    public enum CategoryType
    {
        [EnumItemDescription("一对一")]
        OneToOne = 1,
        [EnumItemDescription("班组")]
        CalssGroup = 2,
        [EnumItemDescription("游学")]
        YouXue = 3,
        [EnumItemDescription("其他")]
        Other = 4,
        [EnumItemDescription("无课收合作")]
        WuKeShou = 5,
    }

    /// <summary>
    /// 产品状态
    /// </summary>
    public enum ProductStatus
    {
        [EnumItemDescription("待审批")]
        PendingApproval =1,
        [EnumItemDescription("审批通过")]
        Approved =2,
        [EnumItemDescription("驳回")]
        Refused =3,
        [EnumItemDescription("销售中")]
        Enabled =4,
        [EnumItemDescription("已停售")]
        Disabled =5

        ///// <summary>
        ///// 草稿
        ///// </summary>
        //[EnumItemDescription("草稿")]
        //New = 5,

        ///// <summary>
        ///// 审批中
        ///// </summary>
        //[EnumItemDescription("审批中")]
        //Approving = 3,

        ///// <summary>
        ///// 已完成
        ///// </summary>
        //[EnumItemDescription("已完成")]
        //Approved = 6,

        ///// <summary>
        ///// 已驳回
        ///// </summary>
        //[EnumItemDescription("已驳回")]
        //Refused = 4,

        ///// <summary>
        ///// 销售中
        ///// </summary>
        //[EnumItemDescription("销售中")]
        //Enabled = 1,

        ///// <summary>
        ///// 禁售
        ///// </summary>
        //[EnumItemDescription("禁售")]
        //Disabled = 2
    }

    /// <summary>
    /// 薪酬规则对象
    /// </summary>
    public enum RuleObject
    {
        [EnumItemDescription("咨询师")]
        Consultant = 1,
        [EnumItemDescription("学管理师")]
        Educator = 2,
        [EnumItemDescription("教师")]
        Teacher = 3,
    }

    public enum ProductUnit
    {
        [EnumItemDescription("课时")]
        Period=1,
        [EnumItemDescription("课次")]
        Lesso=2,
        [EnumItemDescription("期")]
        Issue=3,
        [EnumItemDescription("份")]
        Part=4,
    }

    /// <summary>
    /// 折扣状态
    /// </summary>
    public enum DiscountStatusDefine
    {
        /// <summary>
        /// 待审批
        /// </summary>
        [EnumItemDescription("待审批")]
        Approving = 1,

        /// <summary>
        /// 审批通过
        /// </summary>
        [EnumItemDescription("审批通过")]
        Approved = 2,

        /// <summary>
        /// 驳回
        /// </summary>
        [EnumItemDescription("驳回")]
        Refused = 3,

        /// <summary>
        /// 执行中
        /// </summary>
        [EnumItemDescription("执行中")]
        Enabled = 4,

        /// <summary>
        /// 已停用
        /// </summary>
        [EnumItemDescription("已停用")]
        Disabled = 5,
        /// <summary>
        /// 已删除
        /// </summary>
        [EnumItemDescription("已删除")]
        Deleted =6
    }

    public enum CampusUseInfoDefine {
        [EnumItemDescription("当前使用")]
        DQ =1,
        [EnumItemDescription("历史使用")]
        LS =2
    }


    /// <summary>
    /// 服务费类型
    /// </summary>
    public enum ExpenseTypeDefine
    {
        /// <summary>
        /// 一对一
        /// </summary>
        [EnumItemDescription("一对一")]
        One2One = 1,

        /// <summary>
        /// 班组
        /// </summary>
        [EnumItemDescription("班组")]
        Group,

        /// <summary>
        /// 所有
        /// </summary>
        [EnumItemDescription("所有")]
        All
    }
    
    /// <summary>
    /// 买赠状态
    /// </summary>
    public enum PresentStatusDefine
    {
        /// <summary>
        /// 待审批
        /// </summary>
        [EnumItemDescription("待审批")]
        Approving = 1,

        /// <summary>
        /// 审批通过
        /// </summary>
        [EnumItemDescription("审批通过")]
        Approved = 2,

        /// <summary>
        /// 驳回
        /// </summary>
        [EnumItemDescription("驳回")]
        Refused = 3,

        /// <summary>
        /// 执行中
        /// </summary>
        [EnumItemDescription("执行中")]
        Enabled = 4,

        /// <summary>
        /// 已停用
        /// </summary>
        [EnumItemDescription("已停用")]
        Disabled = 5,
        /// <summary>
        /// 已删除
        /// </summary>
        [EnumItemDescription("已删除")]
        Deleted = 6
    }

    /// <summary>
    /// 折扣类型
    /// </summary>
    public enum DiscountTypeDefine {

        [EnumItemDescription("无折扣")]
        None,
        [EnumItemDescription("拓路")]
        Tunland,
        [EnumItemDescription("特殊")]
        Special,
        [EnumItemDescription("买赠")]
        Present,
        [EnumItemDescription("其它")]
        Other
    }

}
