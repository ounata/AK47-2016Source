﻿using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Products.ViewModels.Presents
{
    public class PresentsQueryResultModel
    {
        public PagedQueryResult<PPTS.Data.Products.Entities.Present, PresentCollection> QueryResult { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
}