﻿using MCS.Library.Data.Adapters;
using MCS.Library.Data.Configuration;
using MCS.Library.SOA.DataObjects;
using MCS.Library.SOA.Security.ADSyncUtilities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCS.Library.SOA.Security.ADSyncUtilities.Adapters
{
	public class ADReverseSynchronizeLogDetailAdapter : UpdatableAndLoadableAdapterBase<ADReverseSynchronizeLogDetail, ADReverseSynchronizeLogDetailCollection>
	{
		public static readonly ADReverseSynchronizeLogDetailAdapter Instance = new ADReverseSynchronizeLogDetailAdapter();

		private ADReverseSynchronizeLogDetailAdapter()
		{
		}

		protected override string GetConnectionName()
		{
			return ConnectionNameMappingSettings.GetConfig().GetConnectionName("PermissionsCenter", "PermissionsCenter");
		}
	}
}
