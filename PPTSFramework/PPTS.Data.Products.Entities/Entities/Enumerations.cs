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
        /// <summary>
        /// 草稿
        /// </summary>
        [EnumItemDescription("草稿")]
        New = 5,

        /// <summary>
        /// 审批中
        /// </summary>
        [EnumItemDescription("审批中")]
        Approving = 3,

        /// <summary>
        /// 已完成
        /// </summary>
        [EnumItemDescription("已完成")]
        Approved = 6,

        /// <summary>
        /// 已驳回
        /// </summary>
        [EnumItemDescription("已驳回")]
        Refused = 4,

        /// <summary>
        /// 销售中
        /// </summary>
        [EnumItemDescription("销售中")]
        Enabled = 1,

        /// <summary>
        /// 禁售
        /// </summary>
        [EnumItemDescription("禁售")]
        Disabled = 2
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
        /// 草稿
        /// </summary>
        [EnumItemDescription("草稿")]
        New = 1,

        /// <summary>
        /// 审批中
        /// </summary>
        [EnumItemDescription("审批中")]
        Approving = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        [EnumItemDescription("已完成")]
        Approved = 3,

        /// <summary>
        /// 已驳回
        /// </summary>
        [EnumItemDescription("已驳回")]
        Refused = 4,

        /// <summary>
        /// 销售中
        /// </summary>
        [EnumItemDescription("执行中")]
        Enabled = 5,

        /// <summary>
        /// 禁售
        /// </summary>
        [EnumItemDescription("已停用")]
        Disabled = 6
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
        One2One,

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
    /// 服务费状态
    /// </summary>
    public enum ExpenseStatusDefine
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [EnumItemDescription("草稿")]
        New = 0,

        /// <summary>
        /// 审批中
        /// </summary>
        [EnumItemDescription("审批中")]
        Approving = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        [EnumItemDescription("已完成")]
        Approved = 2,

        /// <summary>
        /// 已驳回
        /// </summary>
        [EnumItemDescription("已驳回")]
        Refused = 3,

        /// <summary>
        /// 销售中
        /// </summary>
        [EnumItemDescription("执行中")]
        Enabled = 4,

        /// <summary>
        /// 禁售
        /// </summary>
        [EnumItemDescription("已停用")]
        Disabled = 5
    }

    /// <summary>
    /// 买赠状态
    /// </summary>
    public enum PresentStatusDefine
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [EnumItemDescription("草稿")]
        New = 1,

        /// <summary>
        /// 审批中
        /// </summary>
        [EnumItemDescription("审批中")]
        Approving = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        [EnumItemDescription("已完成")]
        Approved = 3,

        /// <summary>
        /// 已驳回
        /// </summary>
        [EnumItemDescription("已驳回")]
        Refused = 4,

        /// <summary>
        /// 销售中
        /// </summary>
        [EnumItemDescription("执行中")]
        Enabled = 5,

        /// <summary>
        /// 禁售
        /// </summary>
        [EnumItemDescription("已停用")]
        Disabled = 6
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
