using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using PPTS.Data.Products.Entities;
using PPTS.WebAPI.Customers.ViewModels.Students;
using MCS.Library.Principal;
using PPTS.Data.Common.Security;
using PPTS.Data.Common.Adapters;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    public class EditableCustomerVisitModel
    {
        public CustomerVisit CustomerVisit
        {
            get;
            set;
        }

        

        public int SelectType
        {
            get;
            set;
        }

        public StudentModel Customer
        {
            get;
            set;
        }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries
        {
            get;
            set;
        }

        public EditableCustomerVisitModel()
        {
            this.Dictionaries = new Dictionary<string, IEnumerable<BaseConstantEntity>>();
        }

        public static EditableCustomerVisitModel Load(string id)
        {
            EditableCustomerVisitModel result = new EditableCustomerVisitModel();

            result.Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerVisit));

            CustomerVisitAdapter.Instance.LoadInContext(id, collection =>
            {
                result.CustomerVisit = collection.FirstOrNull();
            });

            CustomerVisitAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            GenericCustomerAdapter<StudentModel, List<StudentModel>>.Instance.LoadInContext(result.CustomerVisit.CustomerID, customer => result.Customer = customer);

            CustomerServiceAdapter.Instance.GetDbContext().DoAction(context => context.ExecuteDataSetSqlInContext());

            return result;
        }

        public CheckTimeResult CheckTime()
        {
            CheckTimeResult CheckTimeResult_ = new CheckTimeResult();
            EditableCustomerVisitModel tempModel = new EditableCustomerVisitModel();
            tempModel = EditableCustomerVisitModel.Load(this.CustomerVisit.VisitID);

            DateTime tempStart = DateTime.Parse(tempModel.CustomerVisit.CreateTime.ToShortDateString()).AddDays(-3);
            DateTime tempEnd = DateTime.Parse(tempModel.CustomerVisit.CreateTime.ToShortDateString()).AddDays(1);

            if (tempStart > this.CustomerVisit.VisitTime || this.CustomerVisit.VisitTime > tempEnd)
            {
                throw new Exception("回访时间允许选择当前日期及当前日期的前三天");
            }

            //DateTime oldVisitTime = tempModel.CustomerVisit.VisitTime;
            //DateTime newVisitTime = this.CustomerVisit.VisitTime;
            //newVisitTime = DateTime.Parse(newVisitTime.ToShortDateString()).AddDays(-3);

            //if (newVisitTime > oldVisitTime)
            //{
            //    CheckTimeResult_.SetErrorMsg("编辑回访时间不能比原始回访时间大三天");
            //    return CheckTimeResult_;
            //}

            DateTime startConfirmTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
            if (DateTime.Now.Day > 3 && this.CustomerVisit.VisitTime < startConfirmTime)
            {
                CheckTimeResult_.SetErrorMsg("不可编辑历史归档数据，相关问题请咨询教学管理部");
                return CheckTimeResult_;
            }
            if (DateTime.Now.Day <= 3 && this.CustomerVisit.VisitTime < startConfirmTime.AddMonths(-1))
            {
                CheckTimeResult_.SetErrorMsg("不可编辑历史归档数据，相关问题请咨询教学管理部");
                return CheckTimeResult_;
            }

            return CheckTimeResult_;
        }

    }
}