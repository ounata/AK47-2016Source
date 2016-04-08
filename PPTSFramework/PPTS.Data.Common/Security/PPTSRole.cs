using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Security
{
    public class PPTSRole
    {
        public string ID
        {
            get;
            set;
        }

        public string CodeName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PPTSRoleCollection : SerializableEditableKeyedDataObjectCollectionBase<string, PPTSRole>
    {
        public PPTSRoleCollection()
        {
        }

        protected PPTSRoleCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        protected override string GetKeyForItem(PPTSRole item)
        {
            return item.ID;
        }
    }
}
