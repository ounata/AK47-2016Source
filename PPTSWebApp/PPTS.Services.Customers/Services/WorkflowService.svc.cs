using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Mapping;
using MCS.Library.Net.SNTP;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Customers.Models;
using PPTS.Contracts.Customers.Operations;
using PPTS.Data.Common;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPTS.Services.Customers.Services
{
    public class WorkflowService : IWorkflowService
    {
        private void AccountRefundProcess(string applyID, string approverID, string approverName, bool isRefused)
        {
            AccountRefundApply apply = AccountRefundApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply == null)
                throw new Exception(string.Format("退费申请{0}不存在", applyID));

            if (apply.ApplyStatus == ApplyStatusDefine.Approving)
            {
                if (isRefused)
                {
                    MutexReleaser mutex = new MutexReleaser(
                        new MutexReleaseParameter()
                        {
                            BillID = apply.ApplyID
                        });
                    mutex.Release(
                        delegate ()
                        {
                            AccountApproveRefundModel approve = new AccountApproveRefundModel();
                            approve.BillID = apply.ApplyID;
                            approve.IsFinalApprove = true;
                            approve.IsRefused = isRefused;
                            approve.ApproveTime = SNTPClient.AdjustedTime;
                            approve.ApproverID = approverID;
                            approve.ApproverName = approverName;
                            approve.PrepareApprove(apply);
                            new AccountApproveRefundApplyExecutor(approve).Execute();
                        });
                }
                else
                {
                    AccountApproveRefundModel approve = new AccountApproveRefundModel();
                    approve.BillID = apply.ApplyID;
                    approve.IsFinalApprove = true;
                    approve.IsRefused = isRefused;
                    approve.ApproveTime = SNTPClient.AdjustedTime;
                    approve.PrepareApprove(apply);
                    new AccountApproveRefundApplyExecutor(approve).Execute();
                }
            }
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AccountRefundProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            this.AccountRefundProcess(CurrentResourceID, CurrentUserID, CurrentUserName, true);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AccountRefundProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            this.AccountRefundProcess(CurrentResourceID, CurrentUserID, CurrentUserName, false);
        }

        private void AccountTransferProcess(string applyID, string approverID, string approverName, bool isRefused)
        {
            AccountTransferApply apply = AccountTransferApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply == null)
                throw new Exception(string.Format("转让申请{0}不存在", applyID));

            if (apply.ApplyStatus == ApplyStatusDefine.Approving)
            {
                MutexReleaser mutex = new MutexReleaser(
                    new MutexReleaseParameter()
                    {
                        BillID = apply.ApplyID
                    });
                mutex.Release(
                    delegate ()
                    {
                        AccountApproveTransferModel approve = new AccountApproveTransferModel();
                        approve.BillID = apply.ApplyID;
                        approve.IsFinalApprove = true;
                        approve.IsRefused = isRefused;
                        approve.ApproveTime = SNTPClient.AdjustedTime;
                        approve.ApproverID = approverID;
                        approve.ApproverName = approverName;
                        approve.PrepareApprove(apply);
                        new AccountApproveTransferApplyExecutor(approve).Execute();
                    });
            }
        }
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AccountTransferProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            this.AccountTransferProcess(CurrentResourceID, CurrentUserID, CurrentUserName, true);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AccountTransferProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            this.AccountTransferProcess(CurrentResourceID, CurrentUserID, CurrentUserName, false);
        }

        private void StudentTransferProcess(string applyID, string approverID, string approverName, bool isRefused)
        {
            CustomerTransferApply apply = CustomerTransferApplyAdapter.Instance.LoadByApplyID(applyID);
            if (apply == null)
                throw new Exception(string.Format("转学申请{0}不存在", applyID));
            Customer customer = CustomerAdapter.Instance.Load(apply.CustomerID);
            if (customer == null)
                throw new Exception(string.Format("学员ID{0}不存在", apply.CustomerID));

            if (apply.ApplyStatus == ApplyStatusDefine.Approving)
            {
                MutexReleaser mutex = new MutexReleaser(
                    new MutexReleaseParameter()
                    {
                        BillID = apply.ApplyID
                    });
                mutex.Release(
                    delegate ()
                    {
                        StudentApproveTransferModel approve = new StudentApproveTransferModel();
                        approve.BillID = apply.ApplyID;
                        approve.IsFinalApprove = true;
                        approve.IsRefused = isRefused;
                        approve.ApproveTime = SNTPClient.AdjustedTime;
                        approve.ApproverID = approverID;
                        approve.ApproverName = approverName;
                        approve.PrepareApprove(apply);
                        new StudentApproveTransferApplyExecutor(approve).Execute();
                    });
            }
        }
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void StudentTransferProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            this.StudentTransferProcess(CurrentResourceID, CurrentUserID, CurrentUserName, true);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void StudentTransferProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            this.StudentTransferProcess(CurrentResourceID, CurrentUserID, CurrentUserName, false);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ThawProcessCancelling(string CustomerID)
        {
            //作废流程时，需要把客户的解冻中状态，修改为原始状态（解冻）。注意事务完整性
            //  throw new NotImplementedException(string.Format("不支持ThawProcessCancelling: {0}", CustomerID));
            Customer customer = CustomerAdapter.Instance.Load(CustomerID);
            customer.StudentStatus = Data.Customers.StudentStatusDefine.Blocked;
            customer.FillModifier();
            CustomerAdapter.Instance.Update(customer);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ThawProcessCompleting(string CustomerID, ThawReasonType ThawReasonType)
        {
            //流程办结时，执行解冻操作。注意事务完整性
            // throw new NotImplementedException(string.Format("不支持ThawProcessCompleting: {0}", CustomerID));
            Customer customer = CustomerAdapter.Instance.Load(CustomerID);
            customer.StudentStatus = Data.Customers.StudentStatusDefine.Normal;

            if (ThawReasonType == ThawReasonType.Repeat || ThawReasonType == ThawReasonType.Grad)
                customer.Graduated = true;
            customer.FillModifier();
            CustomerAdapter.Instance.Update(customer);
        }
    }
}
