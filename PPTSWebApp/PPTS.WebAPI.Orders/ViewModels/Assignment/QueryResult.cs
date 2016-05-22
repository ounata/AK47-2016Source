using PPTS.Data.Orders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{

    [Serializable]
    [DataContract]
    public class TeacherModel
    {
        #region
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public IList<TchSubjectGradeRela> Tch { get; set; }
        [DataMember]
        public IList<KeyValue> Grade { get; set; }
        [DataMember]
        public IDictionary<string, IList<KeyValue>> GradeSubjectRela { get; set; }

        public TeacherModel()
        {
            Tch = new List<TchSubjectGradeRela>();
            Grade = new List<KeyValue>();
            GradeSubjectRela = new Dictionary<string, IList<KeyValue>>();
        }
        #endregion
    }
    [Serializable]
    [DataContract]
    public class TchSubjectGradeRela
    {
        [DataMember]
        public string Grade { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public IList<KeyValue> Teachers { get; set; }
    }

    [Serializable]
    [DataContract]
    public class KeyValue
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Field01 { get; set; }
    }



    [Serializable]
    [DataContract]
    public class StudentModel
    {
        #region
        [DataMember]
        public IList<KeyValue> Student { get; set; }
        [DataMember]
        public IList<KeyValue> Grade { get; set; }
        [DataMember]
        public IDictionary<string, IList<KeyValue>> GradeSubjectRela { get; set; }

        public StudentModel()
        {
            Student = new List<KeyValue>();
            Grade = new List<KeyValue>();
            GradeSubjectRela = new Dictionary<string, IList<KeyValue>>();
        }

        #endregion

    }

    ///按学员创建排课条件，初始化数据模型
    public class InitDataByStuCAQR : CreateAssignQRBase
    {
        public AssetViewCollection AssignExtension
        {
            get;
            set;
        }

        public TeacherModel Teacher
        {
            get;
            set;
        }

        public InitDataByStuCAQR() : base()
        {

        }
    }
    ///按教师创建排课条件，初始化数据模型
    public class InitDataByTchCAQR : CreateAssignQRBase
    {
        public StudentModel Student
        {
            get;
            set;
        }
        public InitDataByTchCAQR() : base()
        {

        }
    }

    public class SimpleAssetViewCollection
    {
        public AssetViewCollection Result { get; set; }
    }
    public class SimpleAssetView
    {
        public AssetView Result { get; set; }
    }

   
}