using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.ExtServices.CTI.Models.CustomerSource
{
    public class CustomerSourceModel
    {

        public BaseConstantEntity MainSource
        { get; set; }

        public BaseConstantEntity SubSource
        { get; set; }
    }
}