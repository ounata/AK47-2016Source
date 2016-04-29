using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Orders.ViewModels.AssignConditions
{
    public class AssignConditionQueryModel
    {
        /// <summary>
        /// 学区ID
        /// </summary>
        public string CustomerCampusID
        {
            get; set;
        }
        /// <summary>
        /// 学员ID
        /// </summary>
        public string CustomerID
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class TeacherModel
    {
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string TeacherID { get; set; }
        [DataMember]
        public string TeacherName { get; set; }
        [DataMember]
        public string TeacherJobID { get; set; }
        [DataMember]
        public string Grade { get; set; }
    }
}

