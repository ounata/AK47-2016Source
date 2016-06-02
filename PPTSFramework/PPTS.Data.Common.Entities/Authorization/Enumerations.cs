﻿using MCS.Library.Core;
using PPTS.Data.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Authorization
{
    /// <summary>
    /// 操作类型集合
    /// </summary>
    public enum ActionType
    {
        [EnumItemDescription("读操作")]
        Read = 0,
        [EnumItemDescription("写操作")]
        Write = 1
    }

    /// <summary>
    /// 关系类型集合
    /// </summary>
    public enum RelationType
    {
        [EnumItemDescription("建档关系")]
        Owner = 0,
        [EnumItemDescription("资询关系")]
        Consultant = JobTypeDefine.Consultant,
        [EnumItemDescription("学管关系")]
        Educator = JobTypeDefine.Educator,
        [EnumItemDescription("教师关系")]
        Teacher = JobTypeDefine.Teacher,
        [EnumItemDescription("坐席关系")]
        Callcenter = JobTypeDefine.Callcenter,
        [EnumItemDescription("市场关系")]
        Marketing = JobTypeDefine.Marketing,

    }
    
    /// <summary>
    /// 机构类型
    /// </summary>
    public enum OrgType
    {
        [EnumItemDescription("总公司", Category = "DataScope")]
        HQ = DepartmentType.HQ,

        [EnumItemDescription("大区", Category = "DataScope")]
        Region = DepartmentType.Region,

        /// <summary>
        /// 分公司
        /// </summary>
        [EnumItemDescription("分公司", Category = "DataScope")]
        Branch = DepartmentType.Branch,

        /// <summary>
        /// 校区
        /// </summary>
        [EnumItemDescription("校区", Category = "DataScope")]
        Campus = DepartmentType.Campus,

        /// <summary>
        /// 部门
        /// </summary>
        [EnumItemDescription("部门")]
        Department = DepartmentType.Department,

        /// <summary>
        /// 学科组
        /// </summary>
        [EnumItemDescription("学科组", Category = "DataScope")]
        XueKeZu = DepartmentType.XueKeZu
    }

    /// <summary>
    /// 潜客/学员记录类型
    /// </summary>
    public enum CustomerRecordType
    {
        /// <summary>
        /// 潜客记录
        /// </summary>
        [EnumItemDescription("潜客记录")]
        PotentialCustomer =1,
        /// <summary>
        /// 学员记录
        /// </summary>
        [EnumItemDescription("学员记录")]
        Customer = 2
    }

    /// <summary>
    /// 课程记录类型
    /// </summary>
    public enum CourseRecordType
    {
        /// <summary>
        /// 课时(排课)记录
        /// </summary>
        [EnumItemDescription("课时(排课)记录")]
        Assign = 61
    }

    /// <summary>
    /// 记录类型
    /// </summary>
    public enum RecordType
    {
        #region 客户管理部分
        [EnumItemDescription("潜客记录")]
        PotentialCustomer = CustomerRecordType.PotentialCustomer,
        [EnumItemDescription("学员记录")]
        Customer = CustomerRecordType.Customer,
        [EnumItemDescription("家长记录")]
        Parent = 3,
        [EnumItemDescription("跟进记录")]
        CustomerFollow = 4,
        [EnumItemDescription("学情会记录")]
        CustomerMeeting = 5,
        [EnumItemDescription("上门记录")]
        CustomerVerify = 6,
        [EnumItemDescription("回访记录")]
        CustomerVisit = 7,
        [EnumItemDescription("反馈记录")]
        CustomerReply = 8,
        [EnumItemDescription("客服记录")]
        CustomerService = 9,
        #endregion

        #region 账户部分
        [EnumItemDescription("账户")]
        Account = 21,
        [EnumItemDescription("缴费单")]
        AccountChargeApply = 22,
        [EnumItemDescription("退费单")]
        AccountRefundApply = 23,
        [EnumItemDescription("转让申请单")]
        AccountTransferApply = 24,
        [EnumItemDescription("刷卡记录")]
        POSRecord = 25,
        #endregion

        #region 资产部分
        [EnumItemDescription("资产记录")]
        Asset = 41,
        [EnumItemDescription("订购记录")]
        Order = 42,
        [EnumItemDescription("退订记录")]
        DebookOrder = 43,
        #endregion

        #region 课程部分
        [EnumItemDescription("课时(排课)记录")]
        Assign = CourseRecordType.Assign,
        [EnumItemDescription("班组记录")]
        Classe = 62,
        #endregion

        #region 产品部分
        [EnumItemDescription("产品记录")]
        Product =81,
        [EnumItemDescription("拓路折扣规则记录")]
        Discount = 82,
        [EnumItemDescription("服务费规则记录")]
        Expense = 83,
        [EnumItemDescription("买赠规则记录")]
        Present = 84,

        #endregion 
    }
}
