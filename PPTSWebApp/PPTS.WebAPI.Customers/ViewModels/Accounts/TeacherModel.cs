using PPTS.Data.Common.Adapters;
using PPTS.Data.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Customers.ViewModels.Accounts
{
    [DataContract]
    [Serializable]
    public class TeacherModel
    {
        /// <summary>
        /// 教师ID
        /// </summary>
        [DataMember]
        public string TeacherID
        {
            set;
            get;
        }

        /// <summary>
        /// 教师姓名
        /// </summary>
        [DataMember]
        public string TeacherName
        {
            set;
            get;
        }

        /// <summary>
        /// 教师OA编码
        /// </summary>
        [DataMember]
        public string TeacherOACode
        {
            set;
            get;
        }

        [DataMember]
        public List<TeacherJobModel> TeacherJobs
        {
            set;
            get;
        }

        public static TeacherModel Load(string campusID, string oaCode)
        {
            Teacher teacher = TeacherAdapter.Instance.LoadByTeacherOACode(oaCode);
            if (teacher != null)
            {
                TeacherModel model = teacher.ProjectedAs<TeacherModel>();
                model.TeacherJobs = TeacherJobModel.Load(campusID, teacher.TeacherID);
                return model;
            }
            return null;
        }        
    }

    [DataContract]
    public class TeacherJobModel : TeacherJob
    {
        /// <summary>
        /// 教师类型
        /// </summary>
        [DataMember]
        public string TeacherType
        {
            get
            {
                return (Convert.ToInt32(this.IsFullTime)).ToString();
            }
        }

        [DataMember]
        public string Key
        {
            get
            {
                return this.JobID;
            }
        }

        [DataMember]
        public string Value
        {
            get
            {
                return this.JobName;
            }
        }

        public static List<TeacherJobModel> Load(string campusID, string teacherID)
        {
            TeacherJobCollection jobs = TeacherJobAdapter.Instance.LoadCollectionByTeacherID(campusID, teacherID);
            List<TeacherJobModel> list = new List<TeacherJobModel>();
            foreach(TeacherJob job in jobs)
            {
                list.Add(job.ProjectedAs<TeacherJobModel>());
            }
            return list;
        }
    }
}