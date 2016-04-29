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
    }
}