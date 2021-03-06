﻿using PPTS.Contracts.Customers.Models;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.Service
{
    public class CustomerService
    {
        #region 获取

        /// <summary>
        /// 获取用户帐户
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static List<Account> GetAccountbyCustomerId(string customerId)
        {
            return PPTS.Contracts.Proxies.PPTSAccountQueryServiceProxy.Instance.QueryAccountCollectionByCustomerID(customerId).AccountCollection;

        }

        /// 通过学员ID集合，获得学员集合信息
        public static CustomerQueryResult GetCustomer(string customerID)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryCustomerCollectionByCustomerIDs(customerID).CustomerCollection.FirstOrDefault();
        }

        ///学员对应教师关系
        public static TeacherRelationByCustomerQueryResult GetTeacherRelationByCustomer(string customerID)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryCustomerTeacherRelationByCustomerID(customerID);
        }

        ///教师对应学生关系
        public static CustomerRelationByTeacherQueryResult GetCustomerRelationByTeacher(string teacherJobID)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryCustomerTeacherRelationByTeacherJobID(teacherJobID);
        }

        /// <summary>
        /// 获取用户是否扣除过综合服务费
        /// 1：1对1 是否扣除过
        /// 2：班组 是否扣除过
        /// 3：1对1 和 班组 是否扣除过
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Dictionary<int, bool> GetWhetherToDeductServiceChargeByCustomerId(string customerId)
        {
            var result = new Dictionary<int, bool>();
            
            PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryCustomerExpenseByCustomerID(customerId).CustomerExpenseRelationCollection.ForEach(item =>
            {
                if (!result.ContainsKey(Convert.ToInt32(item.ExpenseType)))
                {
                    result.Add(Convert.ToInt32(item.ExpenseType), true);
                }
            });
            
            if (!result.ContainsKey(1)) { result.Add(1, false); }
            if (!result.ContainsKey(2)) { result.Add(2, false); }
            if (!result.ContainsKey(3)) { result.Add(3, false); }
            return result;
        }



        /// <summary>
        /// 获取缴费单信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static List<AccountChargeApply> GetChargePaysByCustomerId(string customerId)
        {
            return PPTS.Contracts.Proxies.PPTSAccountQueryServiceProxy.Instance.QueryAccountChargeCollectionByCustomerID(customerId).AccountChargeCollection.ToList();

        }

        /// <summary>
        /// 获取缴费单信息
        /// </summary>
        /// <param name="chargeApplyId"></param>
        /// <returns></returns>
        public static AccountChargeApply GetChargePayById(string customerId, string chargeApplyId)
        {
            return GetChargePaysByCustomerId(customerId).SingleOrDefault(s => s.ApplyID == chargeApplyId);
        }

        /// <summary>
        /// 获取学员信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Data.Customers.Entities.Customer GetCustomerByCustomerId(string customerId)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryCustomerByID(customerId);
        }

        /// <summary>
        /// 获取学员父母信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Data.Customers.Entities.Parent GetPrimaryParentByCustomerId(string customerId)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryPrimaryParentByCustomerID(customerId).Parent;
        }

        /// <summary>
        /// 获取关联订单扣除综合服务费
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static CustomerExpenseRelation GetCustomerExpenseByOrderId(string orderId)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.GetCustomerExpenseByOrderId(orderId);
        }

        /// <summary>
        /// 获取学员 咨询、学管 师
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static CustomerQueryResult GetCustomerStaffRelationByCustomerId(string customerId)
        {
            return PPTS.Contracts.Proxies.PPTSCustomerQueryServiceProxy.Instance.QueryCustomerCollectionByCustomerIDs(customerId).CustomerCollection[0];
        }

        #endregion

        #region 更改

        /// <summary>
        /// 扣除服务费用
        /// </summary>
        public static void DeductExpenses(Data.Customers.Entities.CustomerExpenseRelationCollection expenseRelations)
        {
            PPTS.Contracts.Proxies.PPTSCustomerUpdateServiceProxy.Instance.DeductExpenses(expenseRelations.ToList());
        }

        

        #endregion 
    }
    
}