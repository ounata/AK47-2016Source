using System;
using System.Runtime.Serialization;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Common.Security;
using MCS.Library.OGUPermission;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVisits
{
    public class CheckTimeResult
    {
        public bool Sucess { get; set; }

        public string Message { get; set; }


        public CheckTimeResult()
        {
            Sucess = true;
            Message = string.Empty;
        }

        public void SetErrorMsg(string msg)
        {
            Message = msg;
            Sucess = false;
        }
    }
}