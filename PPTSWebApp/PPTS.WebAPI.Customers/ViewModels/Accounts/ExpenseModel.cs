using MCS.Library.Data.Mapping;
using PPTS.Data.Customers.Adapters;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    /// <summary>
    /// 服务费模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class ExpenseModel : CustomerExpenseRelation
    {
        /// <summary>
        /// 能否被返还
        /// </summary>
        [DataMember]
        public bool CanReturn
        {
            set;
            get;
        }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Memo
        {
            get
            {
                if (this.CanReturn)
                    return "可以返还";
                else
                    return "已上课，不可返还";
            }
        }

        public static List<ExpenseModel> Load(string customerID)
        {
            List<ExpenseModel> list = new List<ExpenseModel>();
            CustomerExpenseRelationCollection c = CustomerExpenseRelationAdapter.Instance.LoadCollection(customerID);
            foreach (CustomerExpenseRelation e in c)
                list.Add(Load(e));
            return list;
        }

        public static ExpenseModel Load(string customerID, string expenseID)
        {
            CustomerExpenseRelation e = CustomerExpenseRelationAdapter.Instance.Load(customerID, expenseID);
            if (e != null)
                return Load(e);
            return null;
        }

        private static ExpenseModel Load(CustomerExpenseRelation e)
        {
            ExpenseModel model = AutoMapper.Mapper.DynamicMap<ExpenseModel>(e);
            model.CanReturn = true;
            return model;
        }
    }
}