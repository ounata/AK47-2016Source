using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.SOA.DataObjects.Security
{
    /// <summary>
    /// 生成FullPath时的临时结构
    /// </summary>
    internal class SCRelationForFullPath
    {
        [ORFieldMapping("ParentID")]
        public string ParentID
        {
            get;
            set;
        }

        [ORFieldMapping("ObjectID")]
        public string ObjectID
        {
            get;
            set;
        }

        [ORFieldMapping("Name")]
        public string Name
        {
            get;
            set;
        }

        [ORFieldMapping("ChildSchemaType")]
        public string ChildSchemaType
        {
            get;
            set;
        }

        [ORFieldMapping("InnerSort")]
        public long InnerSort
        {
            get;
            set;
        }

        [ORFieldMapping("FullPath")]
        public string FullPath
        {
            get;
            set;
        }

        [ORFieldMapping("GlobalSort")]
        public string GlobalSort
        {
            get;
            set;
        }

        public bool AreFullPathEqual(string parentFullPath, string parentGlobalSort)
        {
            string thisFullPath = this.FullPath ?? string.Empty;
            string thisGlobalSort = this.GlobalSort ?? string.Empty;
            string thisName = this.Name ?? string.Empty;

            return this.FullPath == parentFullPath + "\\" + this.Name &&
                this.GlobalSort == parentGlobalSort + string.Format("{0:0000000000}", this.InnerSort);

        }

        public SCRelationForFullPath Clone()
        {
            SCRelationForFullPath result = new SCRelationForFullPath();

            result.ParentID = this.ParentID;
            result.ObjectID = this.ObjectID;
            result.Name = this.Name;
            result.ChildSchemaType = this.ChildSchemaType;
            result.InnerSort = this.InnerSort;
            result.FullPath = this.FullPath;
            result.GlobalSort = this.GlobalSort;

            return result;
        }
    }

    internal class SCRelationForFullPathCollection : EditableDataObjectCollectionBase<SCRelationForFullPath>
    {
        /// <summary>
        /// 得到需要更新FullPath和GlobalSort的集合
        /// </summary>
        /// <param name="parentFullPath"></param>
        /// <param name="parentGlobalSort"></param>
        /// <returns></returns>
        public SCRelationForFullPathCollection SetAndFilterUnmatched(string parentFullPath, string parentGlobalSort)
        {
            SCRelationForFullPathCollection result = new SCRelationForFullPathCollection();

            foreach(SCRelationForFullPath rfp in this)
            {
                if (rfp.AreFullPathEqual(parentFullPath, parentGlobalSort) == false)
                {
                    rfp.FullPath += "\\" + rfp.Name;
                    rfp.GlobalSort += string.Format("{0:0000000000}", rfp.InnerSort);

                    result.Add(rfp);
                }
            }
 
            return result;
        }
    }
}
