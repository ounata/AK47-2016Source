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
using PPTS.Data.Common;

namespace PPTS.Data.Orders.Adapters
{
    public class AssetViewAdapter : OrderAdapterBase<AssetView, AssetViewCollection>
    {
        public static readonly AssetViewAdapter Instance = new AssetViewAdapter();

        private AssetViewAdapter()
        {
        }

        public AssetViewCollection LoadCollection(string customerID)
        {
            WhereLoadingCondition wLC = new WhereLoadingCondition(builder => builder
            .AppendItem("Amount", 0, ">")
            .AppendItem("CategoryType", (int)CategoryType.OneToOne)
            .AppendItem("CustomerID", customerID));

            return this.Load(wLC);
        }

        public AssetViewCollection LoadCollection(string operaterCampusID, string customerID, string subject, string grade)
        {
            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            wSCB.AppendItem("Amount", 0, ">")
            .AppendItem("CategoryType", (int)CategoryType.OneToOne)
            .AppendItem("CustomerID", customerID)
            .AppendItem("CustomerCampusID", operaterCampusID);
            string cc = string.Format("((subject='{0}' and grade='{1}') or (subject='0' and grade='{1}') or (subject='{0}' and grade='0') or (subject='0' and grade='0'))", subject, grade);
            wSCB.AppendItem(cc, "", "", true);
            WhereLoadingCondition wLC = new WhereLoadingCondition(s => { foreach (var v in wSCB) s.Add(v); });
            return this.Load(wLC);
        }

        public AssetView Load(string customerID, string assetID)
        {
            WhereLoadingCondition wLC = new WhereLoadingCondition(builder => builder
           .AppendItem("Amount", 0, ">").AppendItem("AssetID", assetID)
           .AppendItem("CategoryType", (int)CategoryType.OneToOne)
           .AppendItem("CustomerID", customerID));
            //.AppendItem("CustomerCampusID", operaterCampusID)

            return this.Load(wLC).FirstOrDefault();
        }

        public AssetView Load(string itemId)
        {
            return Load(new WhereLoadingCondition(builder => builder.AppendItem("AssetRefID", itemId))).SingleOrDefault();
        }

    }
}