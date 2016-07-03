using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.ExtServices.UnionPay.Models.Statement
{
    [DataContract]
    public class RequestModel
    {
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime
        {
            get;
            set;
        }

        [DataMember]
        public List<StatementModel> ListStatementModel
        {
            get;
            set;
        }
    }
}