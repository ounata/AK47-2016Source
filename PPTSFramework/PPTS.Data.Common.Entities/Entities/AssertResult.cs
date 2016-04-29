using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS
{
    /// <summary>
    /// 判定结果
    /// </summary>
    [DataContract]
    [Serializable]
    public class AssertResult
    {
        /// <summary>
        /// 判定结果是否满足
        /// </summary>
        [DataMember]
        public bool OK
        {
            set;
            get;
        }

        /// <summary>
        /// 判定返回的消息
        /// </summary>
        [DataMember]
        public string Message
        {
            set;
            get;
        }

        public AssertResult()
        {
            this.OK = true;
        }

        public AssertResult(bool ok, string message)
        {
            this.OK = ok;
            this.Message = message;
        }
    }
}
