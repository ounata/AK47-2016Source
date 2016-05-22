using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    [Serializable]
    [ORTableMapping("SC.SchemaOrganizationSnapshot_Current")]
    [DataContract]
    public class PPTSOrganization
    {
        [ORFieldMapping("ID", PrimaryKey = true)]
        [DataMember]
        public string ID
        { get; set; }

        [ORFieldMapping("Name")]
        [DataMember]
        public string Name
        { get; set; }

        [ORFieldMapping("DisplayName")]
        [DataMember]
        public string DisplayName
        { get; set; }

        [ORFieldMapping("CodeName")]
        [DataMember]
        public string CodeName
        { get; set; }

        [ORFieldMapping("CreateDate")]
        [DataMember]
        public string CreateDate
        { get; set; }

        [ORFieldMapping("ShortName")]
        [DataMember]
        public string ShortName
        { get; set; }

    }

    [Serializable]
    [DataContract]
    public class PPTSOrganizationCollection : EditableDataObjectCollectionBase<PPTSOrganization>
    {

    }
}
