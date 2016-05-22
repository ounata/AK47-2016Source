using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers.Executors;
using PPTS.WebAPI.Customers.ViewModels.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PPTS.Data.Common.Security;
using MCS.Library.Principal;

namespace PPTS.WebAPI.Customers.Executors
{
    [DataExecutorDescription("学大反馈")]
    public class CustomerRepliesExecutor : PPTSEditCustomerExecutorBase<EditCustomerRepliesModel>
    {
       
        public CustomerRepliesExecutor(EditCustomerRepliesModel model) : base(model, null)
        {
            model.CustomerId.NullCheck("model");
            model.Items.NullCheck("model");
        }
        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            var cDateTime= DateTime.Now;
            var customer = CustomerAdapter.Instance.Load(Model.CustomerId);
            var parentRelation = CustomerParentRelationAdapter.Instance.Load(Model.CustomerId).Find(a=>a.IsPrimary==true);
            var parent = ParentAdapter.Instance.Load(parentRelation.ParentID);
            var parentPhones = PhoneAdapter.Instance.LoadByOwnerID(parentRelation.ParentID).Find(a=>a.IsPrimary==true);
            foreach (CustomerReply item in Model.Items)
            {
                item.CampusID = customer.CampusID;
                item.CampusName = customer.CampusName;
                item.CustomerName = customer.CustomerName;
               
                item.ReplyFrom = Convert.ToString((int)ReplyFrom.PPTSWEB);
                item.Poster = Convert.ToString((int)Poster.XUEDA);
                item.CreateTime = cDateTime;
                item.ParentID = parent.ParentID;
                item.ParentName = parent.ParentName;
                item.ReplyID = UuidHelper.NewUuidString();
                item.ReplyTime = cDateTime;
                item.PhoneNumber = parentPhones.PhoneNumber;
                item.CreatorID= DeluxeIdentity.CurrentUser.ID;
                item.CreatorName = DeluxeIdentity.CurrentUser.Name;
                item.ReplierID = DeluxeIdentity.CurrentUser.ID;
                item.ReplierJobID = DeluxeIdentity.CurrentUser.GetCurrentJob().ID;
                item.ReplierName = DeluxeIdentity.CurrentUser.Name;
                //item.ReplyObject = DeluxeIdentity.CurrentUser.GetCurrentJob().Name;
                var organ = DeluxeIdentity.CurrentUser.GetCurrentJob().GetParentOrganizationByType(DepartmentType.Branch);
                item.BranchID = null!= organ? organ.ID:null;
                item.BranchName = null != organ ? organ.Name : null; ;
                CustomerReplyAdapter.Instance.UpdateInContext(item);
            }
        }
    }
}