using System.Runtime.Serialization;

namespace PPTS.ExtServices.UnionPay.Models.Response
{
    [DataContract]
    public class ResponseModel
    {
        /// <summary>
        /// 成功或者失败的标识，0成功，1失败
        /// </summary>
        [DataMember]
        public string Flag
        { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string ErrorMessage
        { get; set; }
    }
}