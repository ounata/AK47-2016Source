using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Service;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.CustomerVisits;
using PPTS.WebAPI.Customers.ViewModels.PotentialCustomers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("客户与员工归属关系Executor")]
    public class EditCustomerStaffRelationsExecutor : PPTSEditCustomerExecutorBase<EditCustomerStaffRelationsModel>
    {
        public EditCustomerStaffRelationsExecutor(EditCustomerStaffRelationsModel model) : base(model, null)
        {
            
        }
        /// <summary>
        /// 新增客户与员工归属关系数据
        /// </summary>
        /// <param name="context"></param>
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            IList<CTUser> cUsersList = new List<CTUser>();

            foreach (var cusStaff in Model.CustomerStaffRelations)
            {
                //删选老师
                if (!cUsersList.Contains(EditCustomerStaffRelationsModel.GetUser(cusStaff.StaffID)))
                {
                    cUsersList.Add(EditCustomerStaffRelationsModel.GetUser(cusStaff.StaffID));
                }
                cusStaff.FillCreator();                
                CustomerStaffRelationAdapter.Instance.UpdateInContext(cusStaff);
            }

            //发送短信和邮件
            foreach (var c in cUsersList)
            {
                foreach (int mt in Model.MessageType)
                {
                    switch (mt)
                    {
                        case (int)RemainType.Email:

                            EmailMessage emailMessage = new EmailMessage(c.Email,
                                "分配咨询师提醒",
                                EditCustomerTransferResourcesModel.GenericContextMessage(RemainType.Email, c.Name));
                            EmailMessageAdapter.Instance.Insert(emailMessage);
                            break;
                        case (int)RemainType.Message:
                            SMSTask.Instance.SendSMSWithAdTask(c.Mobile,
                                EditCustomerTransferResourcesModel.GenericContextMessage(RemainType.Message, c.Name), "PPTS");
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