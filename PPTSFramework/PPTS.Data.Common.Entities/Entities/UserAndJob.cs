using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Common.Entities
{
    [Serializable]
    [DataContract]
    public class UserAndJob
    {
        [DataMember]
        public string UserID
        {
            get;
            set;
        }

        [DataMember]
        public string UserNmae
        {
            get;
            set;
        }

        [DataMember]
        public string UserCodeName
        {
            get;
            set;
        }

        [DataMember]
        public string JobID
        {
            get;
            set;
        }

        [DataMember]
        public string JobName
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class UserAndJobCollection : SerializableEditableKeyedDataObjectCollectionBase<string, UserAndJob>
    {
        public UserAndJobCollection()
        {
        }

        public UserAndJobCollection(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        protected override string GetKeyForItem(UserAndJob item)
        {
            return item.JobID;
        }
    }
}
