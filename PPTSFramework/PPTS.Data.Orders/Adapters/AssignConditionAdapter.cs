using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data.Builder;

namespace PPTS.Data.Orders.Adapters
{
    public class AssignConditionAdapter : AssignAdapterBase<AssignCondition, AssignConditionCollection>
    {
        public static readonly AssignConditionAdapter Instance = new AssignConditionAdapter();

        private AssignConditionAdapter()
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
        public AssignCondition Load(string conditionID)
        {
            return this.Load(builder => builder.AppendItem("AssignCondition", conditionID)).SingleOrDefault();
        }
        /// <summary>
        /// 获取当前学员的排课条件
        /// --->条件：资产表中，未排课数量大于零
        /// </summary>
        /// <param name="operaterCampusID">当前操作人所属校区</param>
        /// <param name="customerID">学员ID</param>
        /// <returns></returns>
        public AssignConditionCollection LoadCollection(string operaterCampusID, string customerID)
        {
            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            //wSCB.AppendItem("CustomerCampusID", operaterCampusID);
            wSCB.AppendItem("CustomerID", customerID);
            wSCB.AppendItem("Amount", 0, ">");
            string aIC = string.Format("(SELECT AssetID FROM Assets_Current WHERE {0})", wSCB.ToSqlString(TSqlBuilder.Instance));

            WhereLoadingCondition wlc = new WhereLoadingCondition(builder => builder.AppendItem("AssetID", aIC, "in", true));
            return this.Load(wlc);
        }
    }

}
