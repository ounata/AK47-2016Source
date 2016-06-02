using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MCS.Library.Data;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using MCS.Library.Data.DataObjects;

namespace PPTS.WebAPI.Customers.ViewModels.PotentialCustomers
{
    #region CustomerScoresQueryResult
    public class CustomerScoresQueryResult
    {
        public bool isLastDayOfMonth { get; set; }
        public PagedQueryResult<CustomerScoresSearchModel, CustomerScoresSearchModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
    #endregion 

    #region CustomerScoresSearchModel
    [Serializable]
    public class CustomerScoresSearchModel : CustomerScore
    {
        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string CustomerStatus { get; set; }

        [DataMember]
        public int ClassRank { get; set; }

        [DataMember]
        public int GradeRank { get; set; }

        [DataMember]
        public bool IsStudyHere { get; set; }

        [DataMember]
        public string ItemID { get; set; }

        [DataMember]
        public decimal PaperScore { get; set; }

        [DataMember]
        public decimal RealScore { get; set; }

        [DataMember]
        public new string Satisficing { get; set; }

        [DataMember]
        public int SortNo { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string TeacherID { get; set; }
        [DataMember]
        public string TeacherName { get; set; }
        [DataMember]
        public string TeacherOACode { get; set; }
        [DataMember]
        public string ConstantStaffName { get; set; }
        [DataMember]
        public string EducatorName { get; set; }
    }

    [Serializable]
    public class CustomerScoresSearchModelCollection : EditableDataObjectCollectionBase<CustomerScoresSearchModel>
    {
    }
    #endregion 
}