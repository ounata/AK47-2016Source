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
    public class TeacherAdapter : UpdatableAndLoadableAdapterBase<Teacher, TeacherCollection>
    {
        public static readonly TeacherAdapter Instance = new TeacherAdapter();

        private TeacherAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.PPTSMetaDataConnectionName;
        }

        public Teacher LoadByTeacherOACode(string oaCode)
        {
            return this.Load(builder => builder.AppendItem("TeacherOACode", oaCode)).SingleOrDefault();
        }
    }
}