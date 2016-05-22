using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Executors
{
    public class ApproveModelBase
    {
        /// <summary>
        /// 业务单据ID
        /// </summary>
        public string BillID
        {
            set;
            get;
        }

        /// <summary>
        /// 业务单据编号
        /// </summary>
        public string BillCode
        {
            set;
            get;
        }

        /// <summary>
        /// 最后审批人ID
        /// </summary>
        public string ApproverID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后审批人姓名
        /// </summary>
        public string ApproverName
        {
            get;
            set;
        }

        /// <summary>
        /// 最后审批人岗位ID
        /// </summary>
        public string ApproverJobID
        {
            get;
            set;
        }

        /// <summary>
        /// 最后审批人岗位名称
        /// </summary>
        public string ApproverJobName
        {
            get;
            set;
        }

        /// <summary>
        /// 最后审批时间
        /// </summary>
        public DateTime ApproveTime
        {
            get;
            set;
        }

        public string ApproveMemo
        {
            set;
            get;
        }

        /// <summary>
        /// 是否最终审批
        /// </summary>
        public bool IsFinalApprove
        {
            set;
            get;
        }

        /// <summary>
        /// 是否拒绝
        /// </summary>
        public bool IsRefused
        {
            set;
            get;
        }
    }
}
