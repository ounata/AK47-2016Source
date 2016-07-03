using MCS.Web.MVC.Library.Models;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.Students
{
    [Serializable]
    public class StudentThawModel
    {
        /// <summary>
        /// 附件集合
        /// </summary>
        public MaterialModelCollection Files { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplyName { get; set; }

        /// <summary>
        /// 申请解冻原因
        /// </summary>
        public ThawReasonType ReasonType{ get; set; }

        /// <summary>
        /// 原因描述
        /// </summary>
        public string ThawReason { get; set; }

        /// <summary>
        /// 学员ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 学员姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 学员编号
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 校区名称
        /// </summary>
        public string CampusName { get; set; }
    }
}
