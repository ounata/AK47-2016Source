﻿using MCS.Library.Data.Executors;
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
using System.Web;

namespace PPTS.WebAPI.Products.Executors
{
    [DataExecutorDescription("审批驳回买赠表")]
    public class RefusedPresentExecutor : PPTSEditProductExecutorBase<string>
    {
        public RefusedPresentExecutor(string model)
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
            Present d = PresentAdapter.Instance.Load(builder => builder.AppendItem("PresentID", Model)).FirstOrDefault();
            d.PresentStatus = Data.Products.PresentStatusDefine.Refused;
            d.ApproverID = CreateUser.ID;
            d.ApproverName = CreateUser.Name;
            d.ApproverJobID = CreateJob.ID;
            d.ApproverJobName = CreateJob.Name;
            d.ApproveTime = SNTPClient.AdjustedTime;
            PresentAdapter.Instance.Update(d);
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