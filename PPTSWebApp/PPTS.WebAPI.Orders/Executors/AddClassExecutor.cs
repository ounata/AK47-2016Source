using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Executors;
using MCS.Library.Principal;
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

        private Class c;

        protected override void PrepareData(DataExecutionContext<UserOperationLogCollection> context)
        {
            base.PrepareData(context);
            CheckResultModel check = Model.CheckCreatClass();
            if (!check.Sucess) {
                throw new Exception(check.Message);
            }

            c  = new Class();
            #region 1.添加班级信息
            c.CampusID = Model.CampusID;
            c.CampusName = Model.CampusName;
            c.ProductID = Model.Product.ProductID;
            c.ProductName = Model.Product.ProductName;
            c.ProductCode = Model.Product.ProductCode;
            c.LessonDurationValue = Model.Product.LessonDurationValue;
            c.LessonCount = Model.Product.LessonCount;
            c.PeriodDurationValue = Model.Product.PeriodDurationValue;
            c.PeriodsOfLesson = Model.Product.PeriodsOfLesson;
            c.ClassName = Data.Orders.Helper.GetClassName(Model.Product.ProductCode, Model.ShortCampusName);
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
                    a.CustomerName = customerQuery.Customer.CustomerName;
                    a.CustomerCode = customerQuery.Customer.CustomerCode;
                    if (consultant != null) {
                        a.ConsultantID = consultant.StaffID;
                        a.ConsultantJobID = consultant.StaffJobID;
                        a.ConsultantName = consultant.StaffName;
                    }
                    if (educator != null) {
                        a.EducatorID = educator.StaffID;
                        a.EducatorJobID = educator.StaffJobID;
                        cli.EducatorName = educator.StaffName;
                    }
                    a.ProductID = Model.Product.ProductID;
                    a.ProductName = Model.Product.ProductName;
                    a.ProductCode = Model.Product.ProductCode;
                    //教室信息暂不处理
                    a.TeacherID = this.Model.TeacherID;
                    a.TeacherName = this.Model.TeacherName;
                    a.Grade = this.Model.Grade;
                    a.GradeName = this.Model.GradeName;
                    a.Subject = this.Model.Subject;
                    a.SubjectName = this.Model.SubjectName;
                    a.DurationValue = Model.Product.LessonDurationValue;
                    a.AssignPrice = asset.Price;
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
                    cli.CustomerCode = customerQuery.Customer.CustomerCode;
                    cli.CustomerName = customerQuery.Customer.CustomerName;
                    cli.CustomerCampusID = customerQuery.Customer.CampusID;
                    cli.CustomerCampusName = customerQuery.Customer.CampusName;
                    cli.CustomerGrade = Model.Grade;
                    cli.CustomerGradeName = Model.GradeName;
                    if (consultant != null) {
                        cli.ConsultantID = consultant.StaffID;
                        cli.ConsultantJobID = consultant.StaffJobID;
                        cli.ConsultantName = consultant.StaffName;
                    }
                    if (educator != null) {
                        cli.EducatorID = educator.StaffID;
                        cli.EducatorJobID = educator.StaffJobID;
                        cli.EducatorName = educator.StaffName;
                    }
                    cli.FillCreator();
                    cli.FillModifier();
                    #endregion
                    ClassLessonItemsAdapter.Instance.UpdateInContext(cli);
                    AssignsAdapter.Instance.UpdateInContext(a);
                }
            }
            #endregion

            #region 扣除资产   
            foreach (var asset in Model.Assets)
            {
                Data.Orders.Entities.Asset at = GenericAssetAdapter<Data.Orders.Entities.Asset, AssetCollection>.Instance.Load(asset.AssetID);
                at.AssignedAmount += Model.StartTimeList.Count;
                GenericAssetAdapter<Data.Orders.Entities.Asset, AssetCollection>.Instance.UpdateInContext(at);
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

        protected override object DoOperation(DataExecutionContext<UserOperationLogCollection> context)
        {
            #region 生成数据权限范围数据
            PPTS.Data.Common.Authorization.ScopeAuthorization<Class>
               .GetInstance(PPTS.Data.Orders.ConnectionDefine.PPTSOrderConnectionName)
               .UpdateAuthInContext(DeluxeIdentity.CurrentUser.GetCurrentJob()
               , DeluxeIdentity.CurrentUser.GetCurrentJob().Organization()
               , c.ClassID
               , PPTS.Data.Common.Authorization.RelationType.Owner);
            #endregion 生成数据权限范围数据

            using (DbContext dbContext = PPTS.Data.Orders.ConnectionDefine.GetDbContext())
            {
                dbContext.ExecuteTimePointSqlInContext();

                if (this.DataAction != null)
                    this.DataAction(this.Model);
            }
            return this.Model;
            //return base.DoOperation(context);
        }

        protected override void Validate()
        {
            base.Validate();
            
        }
    }
}