using MCS.Library.Data;
using MCS.Library.Data.DataObjects;
using MCS.Library.Validation;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PPTS.WebAPI.Customers.ViewModels.CustomerScores
{
    public class CustomerScoresModel
    {
        public bool IsTeacher { get; set; }

        public DateTime ClosingAccountDate { get; set; }

        [ObjectValidator]
        public CustomerScore Score { get; set; }

        [ObjectValidator]
        public CustomerScoreItemCollection ScoreItems { get; set; }

        public TeacherSearchCollection Teachers { get; set; }

        public Customer Customer { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

    }


    #region CustomerScoresBatchQueryResult
    public class CustomerScoresBatchQueryResult
    {
        public bool isLastDayOfMonth { get; set; }
        public bool isTeacher { get; set; }
        public PagedQueryResult<CustomerScoresBatchSearchModel, CustomerScoresBatchSearchModelCollection> QueryResult { get; set; }
        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }
    }
    #endregion 

    #region CustomerScoresBatchSearchModel
    [Serializable]
    public class CustomerScoresBatchSearchModel : Customer
    {
        [DataMember]
        public CustomerScore Scores { get; set; }
        [DataMember]
        public CustomerScoreItemCollection ScoreItems { get; set; }
        [DataMember]
        public TeacherSearchCollection Teachers { get; set; }
    }

    [Serializable]
    public class CustomerScoresBatchSearchModelCollection : EditableDataObjectCollectionBase<CustomerScoresBatchSearchModel>
    {
    }
    #endregion 
    
}