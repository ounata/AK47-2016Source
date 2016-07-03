﻿using MCS.Library.Data;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Purchase
{
    public class AssetConsumeViewCriteriaModel
    {
        [ConditionMapping("AssetRefID")]
        public string AssetRefPID { get; set; }

        [NoMapping]
        public PageRequestParams PageParams
        {
            get; set;
        }

        [NoMapping]
        public OrderByRequestItem[] OrderBy
        {
            get; set;
        }
    }
}