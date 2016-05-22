using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class CheckResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Sucess { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        public CheckResultModel() {
            Sucess = true;
            Message = string.Empty;
        }

        public void SetErrorMsg(string msg) {
            Message = msg;
            Sucess = false;
        }
    }
}