using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PPTS.Data.Common.Entities;

namespace PPTS.ExtServices.LeYu.Models.CustomerSource
{
    public class CustomerSourceModel
    {
        public BaseConstantEntity MainSource
        { get; set; }

        public BaseConstantEntity SubSource
        { get; set; }
    }
}