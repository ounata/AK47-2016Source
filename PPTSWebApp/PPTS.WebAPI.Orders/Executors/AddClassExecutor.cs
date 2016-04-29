using MCS.Library.Core;
using MCS.Library.Data.Executors;
using MCS.Library.SOA.DataObjects;
using PPTS.Contracts.Proxies;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders.Executors;
using PPTS.Data.Products;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Orders.ViewModels.ClassGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Executors
{
    /// <summary>
    /// 新建班级
    /// </summary>
    [DataExecutorDescription("新建班级")]
    public class AddClassExecutor:PPTSEditClassGroupExecutorBase<CreatableClassModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dataAction"></param>
        public AddClassExecutor(CreatableClassModel model)
            : base(model, null)
        {
            model.NullCheck("model");
        }

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);

            Class c = new Class();
            #region 1.添加班级信息
            c.CampusID = Model.CampusID;
            c.CampusName = Model.CampusName;
            c.ProductID = Model.Product.ProductID;
            c.ProductName = Model.Product.ProductName;
            c.ProductCode = Model.Product.ProductCode;
            c.DurationValue = Model.Product.LessonDurationValue;
            c.LessonCount = Model.Product.LessonCount;
            c.ClassName = Helper.GetClassName(Model.Product.ProductCode, Model.CampusName);
            c.ClassID = UuidHelper.NewUuidString();
            c.ClassStatus = ClassStatusDefine.Createed;
            //RoomID  RoomCode  RoomName  暂时不处理
            c.Grade = Model.Grade;
            c.GradeName = Model.GradeName;
            c.Subject = Model.Subject;
            c.SubjectName = Model.SubjectName;
            c.StartTime = Model.StartTimeList[0];
            c.EndTime = Model.StartTimeList.Last().AddMinutes((double)Model.Product.LessonDurationValue);
            c.FinishedLessons = 0;
            c.ClassPeoples = this.Model.CustomerCollection.CustomerCollection.Count;
            c.FillCreator();
            c.FillModifier();
            ClassesAdapter.Instance.UpdateInContext(c);
            #endregion

            #region  2.添加上课信息       
            for(int i=0;i< Model.StartTimeList.Count;i++) 
            {
                DateTime startTime = Model.StartTimeList[i];
                ClassLesson cl = new ClassLesson();
                #region ClassLesson
                cl.ClassID = c.ClassID;
                cl.SortNo = i;
                cl.LessonCode = i.ToString();
                cl.LessonID = UuidHelper.NewUuidString();
                cl.LessonStatus = LessonStatus.Assigned;
                cl.StartTime = startTime;
                cl.EndTime = startTime.AddMinutes((double)Model.Product.LessonDurationValue);
                cl.ConfirmStatus = ConfirmStatusDefine.Unconfirmed;
                cl.ConfirmedPeoples = 0;
                cl.LessonPeoples = Model.CustomerCollection.CustomerCollection.Count;
                //RoomID  RoomCode  RoomName  教师信息咱不处理
                cl.TeacherID = Model.TeacherID;
                cl.TeacherName = Model.TeacherName;
                cl.FillCreator();
                cl.FillModifier();
                #endregion
                ClassLessonsAdapter.Instance.UpdateInContext(cl);                
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
                    a.ProductID = Model.Product.ProductID;
                    a.ProductName = Model.Product.ProductName;
                    a.RoomCode = Model.Product.ProductCode;
                    //教室信息暂不处理
                    a.TeacherID = this.Model.TeacherID;
                    a.TeacherName = this.Model.TeacherName;
                    a.Grade = this.Model.Grade;
                    a.GradeName = this.Model.GradeName;
                    a.Subject = this.Model.Subject;
                    a.SubjectName = this.Model.SubjectName;
                    a.DurationValue = Model.Product.LessonDurationValue;
                    a.Price = asset.Price;
                    a.StartTime = startTime;
                    a.EndTime = startTime.AddMinutes((double)Model.Product.LessonDurationValue);
                    a.FillCreator();
                    a.FillModifier();
                    #endregion
                    #region ClassLessonItem
                    cli.LessonID = cl.LessonID;
                    cli.SortNo = i;
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
                    AssetAdapter.Instance.IncreaseAssignedAmountInContext(asset.AssetID,1,a.CreatorID,a.CreatorName);
                }
            }
            #endregion
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