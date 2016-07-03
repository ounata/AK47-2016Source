using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;
using PPTS.Data.Common;
using PPTS.Data.Common.Service;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using MCS.Library.OGUPermission;
using PPTS.Data.Common.Adapters;
using MCS.Library.Net.SNTP;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("划转资源Executor")]
    public class EditCustomerTransferResourcesExecutor : PPTSEditCustomerExecutorBase<EditCustomerTransferResourcesModel>
    {
        public EditCustomerTransferResourcesExecutor(EditCustomerTransferResourcesModel model) : base(model, null)
        { }
        /// <summary>
        ///划转资源
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            DateTime now = SNTPClient.AdjustedUtcTime;
            IList<CTUser> ctsUserList = new List<CTUser>();
            foreach (var m in Model.CustomerTransferResources)
            {
                var user = DeluxeIdentity.CurrentUser;
                m.TransferID = UuidHelper.NewUuidString();
                m.TransferTime = now;
                m.TransferorName = user.Name;
                m.TransferorJobID = user.GetCurrentJob().ID;
                m.TransferorJobName = user.GetCurrentJob().JobName;
                m.FillCreator();
                CustomerTransferResourcesAdapter.Instance.UpdateInContext(m);

                //更新潜客表归属关系
                PotentialCustomer pc = PotentialCustomerAdapter.Instance.Load(action => { action.AppendItem("CustomerID", m.CustomerID); }, DateTime.MinValue).SingleOrDefault();
                pc.OrgID = m.ToOrgID;
                pc.OrgName = m.ToOrgName;
                pc.OrgType = (OrgTypeDefine)Enum.ToObject(typeof(OrgTypeDefine), Convert.ToInt32(m.OrgType));
                pc.CampusID = m.ToOrgID;
                pc.CampusName = m.ToOrgName;
                PotentialCustomerAdapter.Instance.UpdateInContext(pc);

                SendEmailOrMessage(m.ToOrgID,m.IsCampus);
            }


        }
        /// <summary>
        /// 发送短信和邮件
        /// </summary>
        /// <param name="camOrgId"></param>
        /// <param name="isCampus"></param>
        private void SendEmailOrMessage(string camOrgId, bool isCampus)
        {
            //发送邮件和短信
            //不选择校区：分咨询经理
            //选择校区：校咨询主任
            string jobName = isCampus ? "校咨询主任" : "分咨询经理";
            IEnumerable<IUser> iusers = PPTSOrganizationAdapter.Instance.GetUsersInJobsByOrganizationID(camOrgId, jobName);
            foreach (var u in iusers)
            {
                foreach (int mt in Model.MessageType)
                {
                    switch (mt)
                    {
                        case (int)RemainType.Email:

                            EmailMessage emailMessage = new EmailMessage(u.Email, "划转资源提醒",
                                EditCustomerTransferResourcesModel.GenericContextMessage(RemainType.Email, u.Name));
                            EmailMessageAdapter.Instance.Insert(emailMessage);
                            break;
                        case (int)RemainType.Message:
                            SMSTask.Instance.SendSMSWithAdTask(u.GetUserMobile(),
                                EditCustomerTransferResourcesModel.GenericContextMessage(RemainType.Message, u.Name), "PPTS");
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 更新带版本时间的表必须重写这个方法
        /// </summary>
        /// <param name="dbContext"></param>
        protected override void ExecuteNonQuerySqlInContext(DbContext dbContext)
        {
            dbContext.ExecuteTimePointSqlInContext();
        }


    }
}