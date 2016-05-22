using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.LeYu.Executors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.ExtServices.LeYu.Models.PotentialCustomers
{
    /// <summary>
    /// 创建潜在客户封装信息
    /// </summary>
    [Serializable]
    public class CreatablePortentialCustomerModel
    {
        /// <summary>
        /// 联系方式集合
        /// 主要联系方式和次要联系方式
        /// </summary>
        [DataMember]
        public PhoneCollection PhoneCollection
        { get; set; }
        /// <summary>
        /// 潜客信息
        /// </summary>
        [DataMember]
        public PotentialCustomer PotentialCustomer
        { get; set; }

        /// <summary>
        /// 家长信息
        /// </summary>
        [DataMember]
        public Parent Parent
        { get; set; }

        /// <summary>
        /// 学员家长关系信息
        /// </summary>
        [DataMember]
        public CustomerParentRelation CustomerParentRelation
        { get; set; }

        /// <summary>
        /// 学员与员工关系
        /// </summary>
        [DataMember]
        public CustomerStaffRelation CustomerStaffRelation
        { get; set; }
        /// <summary>
        /// 跟进记录信息
        /// </summary>
        [DataMember]
        public CustomerFollow CustomerFollow
        { get; set; }

        public void UpdatePotentialCustomer()
        {
            AddPotentialCustomerExecutor executor = new AddPotentialCustomerExecutor(this);
            executor.Execute();
        }
    }
}