using System.Runtime.Serialization;

namespace PPTS.ExtServices.UnionPay.Models.Response
{
    [DataContract]
    public class ResponseModel
    {
        /// <summary>
        /// 成功或者失败的标识，true成功，false失败
        /// </summary>
        [DataMember]
        public bool Flag
        { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string ErrorMessage
        { get; set; }
    }
}