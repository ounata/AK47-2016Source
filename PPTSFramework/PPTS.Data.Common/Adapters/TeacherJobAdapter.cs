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
    public class TeacherJobAdapter : UpdatableAndLoadableAdapterBase<TeacherJob, TeacherJobCollection>
    {
        public static readonly TeacherJobAdapter Instance = new TeacherJobAdapter();

        private TeacherJobAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }

        public TeacherJobCollection LoadCollectionByTeacherID(string campusID, string teacherID)
        {
            return this.Load(builder => builder.AppendItem("CampusID", campusID).AppendItem("TeacherID", teacherID));
        }
    }
}