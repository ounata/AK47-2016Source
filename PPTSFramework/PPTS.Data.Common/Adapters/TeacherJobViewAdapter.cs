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
using PPTS.Data.Common.Entities;

namespace PPTS.Data.Common.Adapters
{
	public class TeacherJobViewAdapter : UpdatableAndLoadableAdapterBase<TeacherJobView, TeacherJobViewCollection>
	{
		public static readonly TeacherJobViewAdapter Instance = new TeacherJobViewAdapter();

		private TeacherJobViewAdapter()
		{
		}
        
        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }

        public TeacherJobViewCollection LoadCollection(string campusID, string teacherName)
        {
            WhereLoadingCondition wLC = new WhereLoadingCondition(builder => builder
           .AppendItem("TeacherName", string.Format(" like '%{0}%' ", teacherName), "", true)
           .AppendItem("CampusID", campusID));
            return this.Load(wLC);
        }

        public string GetSql(string colName,string colValue)
        {
            string sql = string.Format(@" exists(select 1 from [MT].[TeacherTeachings] where {0}.TeacherID = [MT].[TeacherTeachings].TeacherID 
                and [MT].[TeacherTeachings].{1}='{2}')",this.GetTableName(), colName, colValue);
            return sql;
        }

        public TeacherJobView Load(string teacherJobID)
        {
           return this.Load(p => p.AppendItem("JobID", teacherJobID, "=")).FirstOrDefault();
        }
    }
}