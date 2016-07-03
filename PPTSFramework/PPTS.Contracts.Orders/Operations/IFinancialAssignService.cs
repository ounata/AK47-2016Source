using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Contracts.Orders.Operations
{
    [ServiceContract]
    public interface IFinancialAssignService
    {
        /// <summary>
        /// 查询汇总课时收入的数据信息，并保存到课时收入表中
        /// </summary>
        [OperationContract]
        void SaveFinancialAssignInfo();

        /// <summary>
        /// 向财务系统推送课时收入数据
        /// </summary>
        [OperationContract]
        void SendFinancialAssignInfo();

        /// <summary>
        /// 查询某年某月的课时收入
        /// </summary>
        /// <param name="statisticalYear">统计年份</param>
        /// <param name="statisticalMonth">统计月份</param>
        [OperationContract(Name = "SaveFinancialAssignInfoByMonth")]
        void SaveFinancialAssignInfo(int statisticalYear, int statisticalMonth);

    }
}
