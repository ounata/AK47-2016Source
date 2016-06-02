using MCS.Library.Data.Executors;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Executors;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PPTS.WebAPI.Products.Executors
{
    [DataExecutorDescription("审批通过折扣表")]
    public class ApprovedPresentExecutor : PPTSEditProductExecutorBase<string>
    {
        public ApprovedPresentExecutor(string model)
            : base(model, null)
        {

        }

        #region 创建人：通过当前用户信息获取
        private PPTSJob _createJob = null;
        PPTSJob CreateJob
        {
            get
            {
                if (_createJob == null)
                {
                    _createJob = DeluxeIdentity.CurrentUser.GetCurrentJob();
                }
                return _createJob;
            }
        }

        private IUser _createUser;
        IUser CreateUser
        {
            get
            {
                if (_createUser == null)
                {
                    _createUser = DeluxeIdentity.CurrentUser;
                }
                return _createUser;
            }
        }
        #endregion

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            //买赠表
            Present d = PresentAdapter.Instance.Load(builder => builder.AppendItem("PresentID", Model)).FirstOrDefault();
            d.PresentStatus = Data.Products.PresentStatusDefine.Approved;
            d.ApproverID = CreateUser.ID;
            d.ApproverName = CreateUser.Name;
            d.ApproverJobID = CreateJob.ID;
            d.ApproverJobName = CreateJob.Name;
            d.ApproveTime = SNTPClient.AdjustedTime;
            PresentAdapter.Instance.UpdateInContext(d);

            //买赠表  校区 关系
            PresentPermissionsApplieCollection dpaList = PresentPermissionsApplyAdapter.Instance.Load(builder => builder.AppendItem("PresentID", d.PresentID));
            //关闭
            StringBuilder sb = new StringBuilder();
            foreach (var item in dpaList)
            {
                sb.AppendFormat(",'{0}'", item.CampusID);
            }
            PresentPermissionCollection dpc = PresentPermissionAdapter.Instance.Load(builder => builder.AppendItem("CampusID", sb.ToString().Substring(1), "in", true));
            foreach (var item in dpc)
            {
                if (item.EndDate > d.StartDate)
                {
                    item.EndDate = d.StartDate;
                    PresentPermissionAdapter.Instance.UpdateInContext(item);
                }
            }
            //放开
            foreach (var item in dpaList)
            {
                PresentPermission dp = new PresentPermission();
                dp.CampusID = item.CampusID;
                dp.PresentID = item.PresentID;
                dp.StartDate = d.StartDate;
                dp.FillCreator();
                PresentPermissionAdapter.Instance.UpdateInContext(dp);
            }
        }

        /// <summary>
        /// 准备日志信息
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareOperationLog(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareOperationLog(context);
        }
    }
}