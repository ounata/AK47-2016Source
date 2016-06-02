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
    /// <summary>
    /// 审批通过折扣表
    /// </summary>
    [DataExecutorDescription("审批通过折扣表")]
    public class ApprovedDiscountExecutor : PPTSEditProductExecutorBase<string>
    {
        public ApprovedDiscountExecutor(string model)
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
            //折扣表
            Discount d =  DiscountAdapter.Instance.Load(builder => builder.AppendItem("DiscountID", Model)).FirstOrDefault();
            d.DiscountStatus = Data.Products.DiscountStatusDefine.Approved;
            d.ApproverID = CreateUser.ID;
            d.ApproverName = CreateUser.Name;
            d.ApproverJobID = CreateJob.ID;
            d.ApproverJobName = CreateJob.Name;
            d.ApproveTime = SNTPClient.AdjustedTime;
            DiscountAdapter.Instance.UpdateInContext(d); 

            //折扣表  校区 关系
            DiscountPermissionsApplieCollection dpaList = DiscountPermissionsApplyAdapter.Instance.Load(builder=>builder.AppendItem("DiscountID", d.DiscountID));
            //关闭
            StringBuilder sb = new StringBuilder();
            foreach (var item in dpaList)
            {
                sb.AppendFormat(",'{0}'",item.CampusID);
            }
            DiscountPermissionCollection dpc = DiscountPermissionAdapter.Instance.Load(builder=>builder.AppendItem("CampusID", sb.ToString().Substring(1),"in",true));
            foreach (var item in dpc)
            {
                if (item.EndDate > d.StartDate)
                {
                    item.EndDate = d.StartDate;
                    DiscountPermissionAdapter.Instance.UpdateInContext(item);
                }
            }
            //放开
            foreach (var item in dpaList)
            {
                DiscountPermission dp = new DiscountPermission();
                dp.CampusID = item.CampusID;
                dp.DiscountID = item.DiscountID;
                dp.StartDate = d.StartDate;
                dp.FillCreator();
                DiscountPermissionAdapter.Instance.UpdateInContext(dp);
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