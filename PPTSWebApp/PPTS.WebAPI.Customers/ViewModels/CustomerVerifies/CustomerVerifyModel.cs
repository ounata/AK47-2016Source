﻿using MCS.Library.Core;
using MCS.Library.Data.Mapping;
using MCS.Library.Net.SNTP;
using MCS.Library.OGUPermission;
using MCS.Library.Validation;
using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using PPTS.Data.Common.Security;
using PPTS.Data.Customers.Entities;
using PPTS.WebAPI.Customers.ViewModels.CustomerFollows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerVerifies
{
    public class CustomerVerifyModel : CustomerVerify
    {
        public void InitVerifier(IUser user)
        {
            this.VerifierID = user.ID;
            this.VerifierName = user.Name;
            this.VerifierJobID = user.GetCurrentJob().ID;
            this.VerifierJobName = user.GetCurrentJob().Name;
            this.VerifyTime = SNTPClient.AdjustedTime;
        }
    }
}
