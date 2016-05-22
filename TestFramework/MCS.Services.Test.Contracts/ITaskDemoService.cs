using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Services.Test.Contracts
{
    [ServiceContract]
    public interface ITaskDemoService
    {
        [OperationContract]
        UserData UpdateUser(UserData data);
    }
}
