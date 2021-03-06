﻿using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PPTS.Data.Customers.Entities
{
    [Serializable]
    [ORTableMapping("CM.CustomerTeacherAssignApplies")]
    [DataContract]
    public class CustomerTeacherAssignApply : IEntityWithCreator
    {
        public CustomerTeacherAssignApply() { }

        [ORFieldMapping("ID", PrimaryKey = true)]
        [DataMember]
        public string ID { get; set; }

        [ORFieldMapping("CustomerTeacherRelationID")]
        [DataMember]
        public string CustomerTeacherRelationID { get; set; }

        [ORFieldMapping("ApplyType")]
        [DataMember]
        [ConstantCategory("C_CODE_ABBR_Customer_Teacher_ApplyType")]
        public string ApplyType { get; set; }

        [ORFieldMapping("CampusID")]
        [DataMember]
        public string CampusID { get; set; }

        [ORFieldMapping("CampusName")]
        [DataMember]
        public string CampusName { get; set; }

        [ORFieldMapping("CustomerID")]
        [DataMember]
        public string CustomerID { get; set; }

        [ORFieldMapping("OldTeacherID")]
        [DataMember]
        public string OldTeacherID { get; set; }

        [ORFieldMapping("OldTeacherJobID")]
        [DataMember]
        public string OldTeacherJobID { get; set; }

        [ORFieldMapping("OldTeacherOACode")]
        [DataMember]
        public string OldTeacherOACode { get; set; }

        [ORFieldMapping("OldTeacherName")]
        [DataMember]
        public string OldTeacherName { get; set; }

        [ORFieldMapping("OldTeacherJobOrgID")]
        [DataMember]
        public string OldTeacherJobOrgID { get; set; }
        
        [ORFieldMapping("OldTeacherJobOrgName")]
        [DataMember]
        public string OldTeacherJobOrgName { get; set; }

        [ORFieldMapping("NewTeacherID")]
        [DataMember]
        public string NewTeacherID { get; set; }

        [ORFieldMapping("NewTeacherJobID")]
        [DataMember]
        public string NewTeacherJobID { get; set; }

        [ORFieldMapping("NewTeacherOACode")]
        [DataMember]
        public string NewTeacherOACode { get; set; }

        [ORFieldMapping("NewTeacherName")]
        [DataMember]
        public string NewTeacherName { get; set; }

        [ORFieldMapping("NewTeacherJobOrgID")]
        [DataMember]
        public string NewTeacherJobOrgID { get; set; }
        
        [ORFieldMapping("NewTeacherJobOrgName")]
        [DataMember]
        public string NewTeacherJobOrgName { get; set; }

        [ORFieldMapping("Reason")]
        [DataMember]
        public string Reason { get; set; }

        [ORFieldMapping("ReasonDescription")]
        [DataMember]
        public string ReasonDescription { get; set; }

        [ORFieldMapping("Status")]
        [DataMember]
        public string Status { get; set; }

        [ORFieldMapping("CreatorID")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorID
        {
            get; set;
        }

        [ORFieldMapping("CreatorName")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.All & ~ClauseBindingFlags.Update)]
        [DataMember]
        public string CreatorName
        {
            get; set;           
        }

        [ORFieldMapping("CreateTime")]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [DataMember]
        public DateTime CreateTime { get; set; }
    }

    public class CustomerTeacherAssignApplyCollection: EditableDataObjectCollectionBase<CustomerTeacherAssignApply> { }
}
