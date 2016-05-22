using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Services.Test.Contracts
{
    [Serializable]
    [DataContract]
    public class UserData
    {
        [DataMember]
        public string UserID
        {
            get;
            set;
        }

        [DataMember]
        public string UserName
        {
            get;
            set;
        }

        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }
}
