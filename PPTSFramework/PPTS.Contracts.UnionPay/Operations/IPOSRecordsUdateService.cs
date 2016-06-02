using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PPTS.Contracts.UnionPay.Operations
{
    [ServiceContract]
    public interface IPOSRecordsUdateService
    {
        [OperationContract]
        void UpdatePOSRecords(string strConfigPath);
    }
}
