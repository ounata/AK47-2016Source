using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Security
{
    /// <summary>
    /// PPTS的岗位信息
    /// </summary>
    [Serializable]
    public class PPTSJob
    {
        /// <summary>
        /// Http Header中
        /// </summary>
        public readonly static string JobHeaderTag = "pptsCurrentJobID";

        private HashSet<string> _Functions = null;

        public static HashSet<string> EmptyFunctions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 所属组织的ID
        /// </summary>
        public string ParentOrganizationID
        {
            get;
            set;
        }

        /// <summary>
        /// 岗位类型
        /// </summary>
        public JobTypeDefine JobType
        {
            get;
            set;
        }
        /// <summary>
        /// 是否主岗
        /// </summary>
        public bool IsPrimary
        { get; set; }

        public HashSet<string> Functions
        {
            get
            {
                if (this._Functions == null)
                    this._Functions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                return this._Functions;
            }
            set
            {
                this._Functions = value;
            }
        }
    }

    [Serializable]
    public class PPTSJobCollection : SerializableEditableKeyedDataObjectCollectionBase<string, PPTSJob>
    {
        public PPTSJobCollection()
        {
        }

        protected PPTSJobCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        protected override string GetKeyForItem(PPTSJob item)
        {
            return item.ID;
        }
    }
}
