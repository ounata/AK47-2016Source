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
        /// 通过检索条件获得对应的学员教师关系
        /// </summary>
        /// <param name="QueryModel">查询条件,可选择是否包含客户信息</param>
        /// <returns></returns>
        [OperationContract]
        CustomerTeacherRelationQueryResult QueryCustomerTeacherRelationByCustomerID(CustomerTearcherRelationQueryModel QueryModel);

        /// <summary>
        /// 通过CustomerIDs获得员工及其关系信息
        /// </summary>
        /// <param name="CustomerIDs"></param>
        /// <returns></returns>
        [OperationContract]
        CustomerCollectionQueryResult QueryCustomerCollectionByCustomerIDs(string[] CustomerIDs);
    }
}
