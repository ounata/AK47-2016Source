using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Executors
{
    [DataExecutorDescription("增加学生")]
    public class AddCustomerExecutor : PPTSEditClassGroupExecutorBase<AddCustomerModel>
    {
        public AddCustomerExecutor(AddCustomerModel Model)
            : base(Model, null)
        {

        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            Class c = ClassesAdapter.Instance.LoadByClassID(Model.ClassID);            
            IList<ClassLesson> clc = ClassLessonsAdapter.Instance.LoadCollectionByClassID(Model.ClassID).Where(cl=>cl.LessonStatus == LessonStatus.Assigned).ToList();
            c.ClassPeoples = c.ClassPeoples + Model.Assets.Count();
            ClassesAdapter.Instance.UpdateInContext(c);
            foreach (var item in clc)
            {
                foreach (var asset in Model.Assets)
                {
                    ClassLessonItem cli = new ClassLessonItem();
                    Assign a = new Assign();
                    var customerQuery = Model.CustomerCollection.CustomerCollection.Find(result => result.Customer.CustomerID == asset.CustomerID);
                    var consultant = customerQuery.CustomerStaffRelationCollection.Find(r => r.RelationType == CustomerRelationType.Consultant);
                    var educator = customerQuery.CustomerStaffRelationCollection.Find(r => r.RelationType == CustomerRelationType.Educator);
                    #region Assign
                    a.AssignID = UuidHelper.NewUuidString();
                    a.AssignStatus = AssignStatusDefine.Assigned;
                    a.AssignSource = AssignSourceDefine.Automatic;
                    a.ConfirmStatus = ConfirmStatusDefine.Unconfirmed;
                    a.AssetID = asset.AssetID;
                    a.AssetCode = asset.AssetCode;
                    a.CustomerID = asset.CustomerID;
                    a.CustomerID = customerQuery.Customer.CustomerID;
                    a.CustomerName = customerQuery.Customer.CustomerName;
                    a.CustomerCode = customerQuery.Customer.CustomerCode;
                    a.ConsultantID = consultant.StaffID;
                    a.ConsultantJobID = consultant.StaffJobID;
                    a.ConsultantName = consultant.StaffName;
                    a.EducatorID = educator.StaffID;
                    a.EducatorJobID = educator.StaffJobID;
                    cli.EducatorName = educator.StaffName;
                    a.ProductID = c.ProductID;
                    a.ProductName = c.ProductName;
                    a.ProductCode = c.ProductCode;
                    //教室信息暂不处理
                    a.TeacherID = item.TeacherID;
                    a.TeacherName = item.TeacherName;
                    a.Grade = c.Grade;
                    a.GradeName = c.GradeName;
                    a.Subject = c.Subject;
                    a.SubjectName = c.SubjectName;
                    a.DurationValue = c.LessonDurationValue;
                    a.AssignPrice = asset.Price;
                    a.StartTime = item.StartTime;
                    a.EndTime = item.EndTime;
                    a.FillCreator();
                    a.FillModifier();
                    #endregion
                    #region ClassLessonItem
                    cli.LessonID = item.LessonID;
                    cli.SortNo = item.SortNo;
                    cli.AssignID = a.AssignID;
                    cli.AssignStatus = AssignStatusDefine.Assigned;
                    cli.ConfirmStatus = ConfirmStatusDefine.Unconfirmed;
                    cli.AssetID = asset.AssetID;
                    cli.AssetCode = asset.AssetCode;
                    cli.CustomerID = asset.CustomerID;
                    cli.CustomerID = asset.CustomerID;
                    cli.CustomerCode = customerQuery.Customer.CustomerCode;
                    cli.CreatorName = customerQuery.Customer.CreatorName;
                    cli.CustomerCampusID = customerQuery.Customer.CampusID;
                    cli.CustomerCampusName = customerQuery.Customer.CampusName;
                    cli.ConsultantID = consultant.StaffID;
                    cli.ConsultantJobID = consultant.StaffJobID;
                    cli.ConsultantName = consultant.StaffName;
                    cli.EducatorID = educator.StaffID;
                    cli.EducatorJobID = educator.StaffJobID;
                    cli.FillCreator();
                    cli.FillModifier();
                    #endregion
                    ClassLessonItemsAdapter.Instance.UpdateInContext(cli);
                    AssignsAdapter.Instance.UpdateInContext(a);

                    //扣除资产
                    AssetAdapter.Instance.IncreaseAssignedAmountInContext(asset.AssetID, 1, a.CreatorID, a.CreatorName);
                }
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