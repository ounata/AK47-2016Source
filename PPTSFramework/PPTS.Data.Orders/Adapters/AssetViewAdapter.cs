using MCS.Library.Caching;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data;

namespace PPTS.Data.Orders.Adapters
{
	public class AssetViewAdapter : OrderAdapterBase<AssetView, AssetViewCollection>
	{
		public static readonly AssetViewAdapter Instance = new AssetViewAdapter();

		private AssetViewAdapter()
		{
		}        
	}
}