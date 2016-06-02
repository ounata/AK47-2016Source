using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Data.Adapters;
using PPTS.Data.Orders.Entities;
using MCS.Library.Data.Builder;
using MCS.Library.Core;

namespace PPTS.Data.Orders.Adapters
{
    public class AssignConditionAdapter : OrderAdapterBase<AssignCondition, AssignConditionCollection>
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
            return this.Load(builder => builder.AppendItem("ConditionID", conditionID)).SingleOrDefault();
        }
        /// <summary>
        /// 获取当前学员的排课条件
        /// --->条件：资产表中，未排课数量大于零
        /// </summary>
        /// <param name="AssignTypeDefine">指明ID的含义</param>
        /// <param name="ID">学员ID或者教师ID</param>
        /// <param name="tchJobID">教师岗位ID</param>
        /// <returns></returns>
        public AssignConditionCollection LoadCollection(AssignTypeDefine atd, string ID, string tchJobID)
        {
            ID.NullCheck("ID不能为空");
            if (atd == AssignTypeDefine.ByTeacher)
                tchJobID.NullCheck("tchJobID不能为空");

            WhereSqlClauseBuilder wSCB = new WhereSqlClauseBuilder();
            switch (atd)
            {
                case AssignTypeDefine.ByStudent:
                    wSCB.AppendItem("CustomerID", ID);
                    break;
                case AssignTypeDefine.ByTeacher:
                    wSCB.AppendItem("TeacherID", ID);
                    wSCB.AppendItem("TeacherJobID", tchJobID);
                    break;
            }

            string aIC = string.Format(" (select 1 from OM.Assets_Current b where b.AssetID = {0}.AssetID and b.Amount>0 )", this.GetTableName());
            wSCB.AppendItem("exists",aIC,"",true);

            WhereLoadingCondition wLC = new WhereLoadingCondition(p => { foreach (var v in wSCB) { p.Add(v); }});

            return this.Load(wLC);  
        }

        public void DeleteCollection(AssignConditionCollection acc)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in acc)
            {
                sb.AppendFormat(",'{0}'",v.ConditionID);
            }
            string where = string.Empty;
            if (sb.Length == 0)
                return;

            where = string.Format("({0})",sb.ToString().Substring(1));
            this.Delete(p => p.AppendItem("ConditionID", where, "in",true));
        }
    }

}
