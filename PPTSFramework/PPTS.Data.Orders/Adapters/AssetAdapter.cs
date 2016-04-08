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

namespace PPTS.Data.Orders.Adapters
{
	public class AssetAdapter : OrderAdapterBase<Asset, AssetCollection>
	{
		public static readonly AssetAdapter Instance = new AssetAdapter();

		private AssetAdapter()
		{
		}

		/// <summary>
		/// 插入操作
		/// </summary>
		/// <param name="asset"></param>
		/*
		public void Insert(Asset asset)
		{
			this.InnerInsert(asset, new Dictionary<string, object>());
		}
		*/

		/// <summary>
		/// 加载操作
		/// </summary>
        /// <param name="assetid"></param>
		/// <returns></returns>
		public Asset Load(string assetid)
		{
            return this.Load(builder => builder.AppendItem("AssetID", assetid)).SingleOrDefault();
		}
        
        protected override string GetConnectionName()
        {
            throw new NotImplementedException();
        }
	}
}