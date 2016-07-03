using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Orders.Adapters
{
    public class AssetConsumeViewAdapter : OrderAdapterBase<AssetConsumeView, AssetConsumeViewCollection>
    {
        public static readonly AssetConsumeViewAdapter Instance = new AssetConsumeViewAdapter();

        private AssetConsumeViewAdapter()
        {
        }


    }
}
