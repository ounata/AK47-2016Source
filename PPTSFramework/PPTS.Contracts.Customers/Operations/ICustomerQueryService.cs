using PPTS.Contracts.Customers.Models;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Customers.Operations
{
    [ServiceContract]
    public interface ICustomerQueryService
    {
        [OperationContract]
        Customer QueryCustomerByID(string customerID);

        /// <summary>
        /// 获取客户主监护人信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [OperationContract]
        PrimaryParentQueryResult QueryPrimaryParentByCustomerID(string customerID);

        /// <summary>
        /// 通过检索条件获得对应的学员教师关系
        /// </summary>
        /// <param name="queryModel">查询条件,可选择是否包含客户信息</param>
        /// <returns></returns>
        //[OperationContract]
        //CustomerTeacherRelationQueryResult QueryCustomerTeacherRelationByCustomerID(CustomerTearcherRelationQueryModel queryModel);

        /// <summary>
        /// 通过学员ID条件获得对应的学员教师关系对象
        /// </summary>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        [OperationContract]
        TeacherRelationByCustomerQueryResult QueryCustomerTeacherRelationByCustomerID(string customerID);

        /// <summary>
        /// 通过教师岗位ID获得对应的学员教师关系对象
        /// </summary>
        /// <param name="teacherJobID">教师岗位ID</param>
        /// <returns></returns>
        [OperationContract]
        CustomerRelationByTeacherQueryResult QueryCustomerTeacherRelationByTeacherJobID(string teacherJobID);

        /// <summary>
        /// 通过CustomerIDs获得员工及其关系信息
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        [OperationContract]
        CustomerCollectionQueryResult QueryCustomerCollectionByCustomerIDs(params string[] customerIDs);

        /// <summary>
        /// 通过CustomerID获得学员综合服务费扣减情况
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [OperationContract]
        CustomerExpenseCollectionQueryResult QueryCustomerExpenseByCustomerID(string customerID);

        /// <summary>
        /// 通过CustomerID获得学员综合服务费扣减情况
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [OperationContract]
        CustomerExpenseCollectionQueryResult QueryCustomerExpenseByCustomerIDs(params string[] customerIDs);

        /// <summary>
        /// 获取关联订单 综合服务费
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [OperationContract]
        CustomerExpenseRelation GetCustomerExpenseByOrderId(string orderID);
    }
}
