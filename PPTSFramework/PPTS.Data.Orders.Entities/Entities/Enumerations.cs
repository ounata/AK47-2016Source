using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders
{
    public enum OrderStatus
    {
        [EnumItemDescription("待审")]
        PendingApproval = 1,
        [EnumItemDescription("审批通过")]
        ApprovalPass = 2,
        [EnumItemDescription("驳回")]
        Reject = 3,
        [EnumItemDescription("已退")]
        Returned = 4,
    }

    public enum DebookStatus
    {
        [EnumItemDescription("待审")]
        PendingApproval = 1,
        [EnumItemDescription("审批通过")]
        ApprovalPass = 2,
        [EnumItemDescription("驳回")]
        Reject = 3,
        [EnumItemDescription("已退")]
        Returned = 4,
    }

    /// <summary>
    /// 排课状态（1-排定，3-已上，8-异常，10-无效）
    /// </summary>
    public enum AssignStatusDefine
    {
        [EnumItemDescription("排定")]
        Assigned = 1,
        [EnumItemDescription("已上")]
        Finished = 3,
        [EnumItemDescription("异常")]
        Exception = 8,
        [EnumItemDescription("无效")]
        Invalid = 10,
    }
    /// <summary>
    /// 排课来源（0-自动【班组】，1-手工【一对一】，2-补录）
    /// </summary>
    public enum AssignSourceDefine
    {
        [EnumItemDescription("自动【班组】")]
        Automatic = 0,
        [EnumItemDescription("手工【一对一】")]
        Manual = 1,
        [EnumItemDescription("补录")]
        Makeup = 2
    }
    /// <summary>
    /// 排课类型
    /// </summary>
    public enum AssignTypeDefine
    {
        [EnumItemDescription("按照学员排课")]
        ByStudent,
        [EnumItemDescription("按照教师排课")]
        ByTeacher
    }

    /// <summary>
    /// 确认状态（0-未确认，1-已确认，3-已删除，4-部分确认-针对班组班级课程确认状态时有效）
    /// </summary>
    public enum ConfirmStatusDefine
    {
        [EnumItemDescription("未确认")]
        Unconfirmed = 0,
        [EnumItemDescription("已确认")]
        Confirmed = 1,
        [EnumItemDescription("已删除")]
        Deleted = 3,
        [EnumItemDescription("部分确认-针对班组班级课程确认状态时有效）")]
        ConfirmedPartial = 4
    }

    /// <summary>
    /// 班级状态
    /// </summary>
    public enum ClassStatusDefine {
        [EnumItemDescription("新建")]
        Createed =0,
        [EnumItemDescription("已上部分")]
        Part =1,
        [EnumItemDescription("已上完")]
        All =2,
        [EnumItemDescription("已删除")]
        Deleted =9
    }

    /// <summary>
    /// 课次状态
    /// </summary>
    public enum LessonStatus {
        [EnumItemDescription("排定")]
        Assigned =1,
        [EnumItemDescription("已上")]
        Finished =3,
        [EnumItemDescription("已删除")]
        Deleted =9
    }

    public enum OrderType
    {

        [EnumItemDescription("常规订购")]
        Ordinary=1,
        [EnumItemDescription("买赠订购")]
        Freebie =2,
        [EnumItemDescription("插班订购")]
        Transfer = 3,
        [EnumItemDescription("补差兑换")]
        Makeup = 4,
        [EnumItemDescription("不补差兑换")]
        NoMakeup = 5,
    }

}
