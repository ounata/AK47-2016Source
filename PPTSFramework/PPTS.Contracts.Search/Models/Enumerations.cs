using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Search.Models
{
    /// <summary>
    /// 服务更新类型
    /// </summary>
    public enum CustomerSearchUpdateType
    {
        [EnumItemDescription("更新客户服务")]
        Customer,
        [EnumItemDescription("更新学员员工关系服务")]
        CustomerStaffRelation,
        [EnumItemDescription("更新学员教师关系服务")]
        CustomerTeacherRelation,
        [EnumItemDescription("更新学员家长服务")]
        CustomerParentRelation,
        [EnumItemDescription("更新学员电话服务")]
        CustomerPhone,
        [EnumItemDescription("更新家长电话服务")]
        ParentPhone,
        [EnumItemDescription("更新学员学校服务")]
        CustomerSchoolRelation,
        [EnumItemDescription("更新学员上门服务")]
        CustomerVerify,
        [EnumItemDescription("更新学员成绩服务")]
        CustomerScore,
        [EnumItemDescription("更新缴费单服务")]
        AccountChargeApply,
        [EnumItemDescription("更新学员退费单服务")]
        AccountRefundApply,
        [EnumItemDescription("更新学员账户服务")]
        Account,
        [EnumItemDescription("更新学员综合服务费扣除服务")]
        CustomerExpenseRelation,
        [EnumItemDescription("更新学员转让服务")]
        AccountTransferApply,
        [EnumItemDescription("更新学员资产服务")]
        Asset,
        [EnumItemDescription("更新学员订购服务")]
        Order,
        [EnumItemDescription("更新学员退订服务")]
        DebookOrder,
        [EnumItemDescription("更新学员课时服务")]
        Assign,
        [EnumItemDescription("更新学员跟进服务")]
        CustomerFollow
    }
}
