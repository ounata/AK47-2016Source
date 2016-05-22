using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.CTI.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.ExtServices.CTI.Models.PotentialCustomers
{
    [Serializable]
    public class PotentialCustomerModel
    {
        /// <summary>
        /// 潜客信息
        /// </summary>
        public PotentialCustomer PotentialCustomer
        { get; set; }

        /// <summary>
        /// 家长信息
        /// </summary>
        public Parent Parent
        { get; set; }

        /// <summary>
        /// 学员与家长关系
        /// </summary>
        public CustomerParentRelation CustomerParentRelation
        { get; set; }

        /// <summary>
        /// 家长联系方式信息
        /// </summary>
        public PhoneCollection PhoneCollection
        { get; set; }
        
        /// <summary>
        /// 学员与在读学校关系
        /// </summary>
        public CustomerSchoolRelation CustomerSchoolRelation
        { get; set; }

        /// <summary>
        /// 学员与员工关系
        /// </summary>
        public CustomerStaffRelation CustomerStaffRelation
        { get; set; }

        /// <summary>
        /// 跟进记录
        /// </summary>
        public CustomerFollow CustomerFollow
        { get; set; }

        public void UpdatePotentialCustomer()
        {
            AddPotentialCustomerExecutor executor = new AddPotentialCustomerExecutor(this);
            executor.Execute();
        }
    }
}