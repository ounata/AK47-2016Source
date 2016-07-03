using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    public class PosRecordResult
    {
        /// <summary>
        /// 是否读取成功
        /// </summary>
        public bool OK
        {
            get;
            set;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        public PosRecordModel Record
        {
            set;
            get;
        }

        public static PosRecordResult Load(string campusID, string payTicket, string payType)
        {
            PosRecordModel model = PosRecordModel.Load(campusID, payTicket, payType);
            if (model == null)
                return new PosRecordResult() { Message = "该交易参考流水号不存在" };
            if (model.IsUsered)
                return new PosRecordResult() { Message = "该交易参考流水号已被使用" };

            return new PosRecordResult() { OK = true, Record = model };
        }
    }
}