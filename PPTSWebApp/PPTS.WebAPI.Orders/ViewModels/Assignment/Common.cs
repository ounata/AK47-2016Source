using PPTS.Contracts.Customers.Models;
using PPTS.Data.Common;
using PPTS.Data.Common.Entities;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.Assignment
{
    public class OrderCommonHelper
    {
        public class TmpTeacherModel
        {
            public string Grade { get; set; }
            public string GradeName { get; set; }
            public string Subject { get; set; }
            public string SubjectName { get; set; }
            public string TeacherID { get; set; }
            public string TeacherJobID { get; set; }
            public string TeacherName { get; set; }
            public string JobOrgID { get; set; }
            public string JobOrgName { get; set; }
            public int IsFullTimeTeacher
            {
                get; set;
            }
        }

        public TeacherModel GetTeacher(string stuID, string campusID, Dictionary<string, IEnumerable<BaseConstantEntity>> dic)
        {
            IEnumerable<BaseConstantEntity> grade = dic.Where(p => p.Key.ToLower() == "C_CODE_ABBR_CUSTOMER_GRADE".ToLower()).FirstOrDefault().Value;
            IEnumerable<BaseConstantEntity> subject = dic.Where(p => p.Key.ToLower() == "C_CODE_ABBR_BO_Product_TeacherSubject".ToLower()).FirstOrDefault().Value;

            TeacherRelationByCustomerQueryResult result = PPTS.WebAPI.Orders.Service.CustomerService.GetTeacherRelationByCustomer(stuID);

            TeacherModel teacher = new TeacherModel();
            teacher.CustomerID = stuID;

            if (result.TeacherJobCollection == null)
                return teacher;

            ///当前校区的老师集合
            #region
            var tchs = result.TeacherJobCollection.Where(p => p.TeacherJob.CampusID == campusID);
            var tmpTeacher = new List<TmpTeacherModel>();
            foreach (var tch in tchs)///循环老师
            {
                foreach (var gs in tch.TeacherTeachingCollection)///循环当前老师年级科目关系
                {
                    var grades = grade.Where(p => p.Key == gs.Grade);
                    if (grades == null)
                        continue;
                    var subjects = subject.Where(p => p.Key == gs.Subject);
                    var t = new TmpTeacherModel
                    {
                        Grade = gs.Grade,
                        //GradeName = grade.Where(p => p.Key == gs.Grade).First().Value,
                        Subject = gs.Subject,
                        //SubjectName = bce.Value,
                        TeacherID = tch.TeacherJob.TeacherID,
                        TeacherJobID = tch.TeacherJob.JobID,
                        TeacherName = tch.TeacherJob.TeacherName,
                        JobOrgID = tch.TeacherJob.JobOrgID,
                        JobOrgName = tch.TeacherJob.JobOrgName,
                        IsFullTimeTeacher = tch.TeacherJob.IsFullTime
                    };
                    t.GradeName = grades.First().Value;
                    if (subject != null)
                        t.SubjectName = subjects.First().Value;
                    ///教师 科目 年级关系
                    tmpTeacher.Add(t);
                }
            }
            if (tmpTeacher.Count() == 0)
                return teacher;
            #endregion

            ///年级集合
            var gradeCollection = from grd in tmpTeacher
                                  group grd.Grade by new { grd.Grade, grd.GradeName } into g
                                  select new KeyValue() { Key = g.Key.Grade, Value = g.Key.GradeName };

            teacher.Grade = gradeCollection.ToList();
            ///年级对应科目集合
            #region
            foreach (var v in gradeCollection)
            {
                var subjects = from tch in tmpTeacher
                               where tch.Grade == v.Key
                               group tch by new { tch.Subject, tch.SubjectName } into g
                               select new KeyValue() { Key = g.Key.Subject, Value = g.Key.SubjectName };

                teacher.GradeSubjectRela.Add(v.Key, subjects.ToList());

                ///年级，科目对应的教师集合
                foreach (var sj in subjects)
                {
                    var person = from p in tmpTeacher
                                 where p.Grade == v.Key && p.Subject == sj.Key
                                 select new TeacherInfo()
                                 {
                                     Key = p.TeacherID,
                                     Value = p.TeacherName,
                                     Field01 = p.TeacherJobID,
                                     TeacherJobOrgID = p.JobOrgID,
                                     TeacherJobOrgName = p.JobOrgName,
                                     IsFullTimeTeacher = p.IsFullTimeTeacher
                                 };

                    teacher.Tch.Add(new TchSubjectGradeRela()
                    {
                        Grade = v.Key,
                        Subject = sj.Key,
                        Teachers = person.ToList()
                    });
                }
            }
            #endregion
            return teacher;
        }

        public StudentModel GetStudent(string teacherJobID, string campusID, Dictionary<string, IEnumerable<BaseConstantEntity>> dic)
        {
            StudentModel student = new StudentModel();
            CustomerRelationByTeacherQueryResult result = PPTS.WebAPI.Orders.Service.CustomerService.GetCustomerRelationByTeacher(teacherJobID);
            if (result == null || result.CustomerCollection.Count() == 0)
                return student;
            student.TeacherJobOrgID = result.TeacherJob.TeacherJob.JobOrgID;
            student.TeacherJobOrgName = result.TeacherJob.TeacherJob.JobOrgName;
            ///挑选本校的学员,并且不被冻结状态
            var cusCollction = result.CustomerCollection.Where(p => p.CampusID == campusID && p.StudentStatus != Data.Customers.StudentStatusDefine.Blocked);
            if (cusCollction == null || cusCollction.Count() == 0)
                return student;
            //学员集合
            foreach (var cus in cusCollction)
            {
                student.Student.Add(new KeyValue() { Key = cus.CustomerID, Value = cus.CustomerName, Field01 = cus.CustomerCode });
            }
            IEnumerable<BaseConstantEntity> grade = dic.Where(p => p.Key.ToLower() == "C_CODE_ABBR_CUSTOMER_GRADE".ToLower()).FirstOrDefault().Value;
            IEnumerable<BaseConstantEntity> subject = dic.Where(p => p.Key.ToLower() == "C_CODE_ABBR_BO_Product_TeacherSubject".ToLower()).FirstOrDefault().Value;
            var tmpGradeSubject = new List<TmpTeacherModel>();
            foreach (var gs in result.TeacherJob.TeacherTeachingCollection)///循环年级科目关系
            {
                ///教师 科目 年级关系
                #region
                tmpGradeSubject.Add(new TmpTeacherModel
                {
                    Grade = gs.Grade,
                    GradeName = grade.Where(p => p.Key == gs.Grade).First().Value,
                    Subject = gs.Subject,
                    SubjectName = subject.Where(p => p.Key == gs.Subject).First().Value
                });
                #endregion
            }
            ///年级集合
           #region
            var gradeCollection = from grd in tmpGradeSubject
                                  group grd.Grade by new { grd.Grade, grd.GradeName } into g
                                  select new KeyValue() { Key = g.Key.Grade, Value = g.Key.GradeName };
            student.Grade = gradeCollection.ToList();
            #endregion
            //年级对应科目集合
            #region
            foreach (var v in gradeCollection)
            {
                var subjects = from tch in tmpGradeSubject
                               where tch.Grade == v.Key
                               group tch by new { tch.Subject, tch.SubjectName } into g
                               select new KeyValue() { Key = g.Key.Subject, Value = g.Key.SubjectName };
                student.GradeSubjectRela.Add(v.Key, subjects.ToList());
            }
            #endregion
            return student;
        }


        public void GetCourseCondition(AssignQCM qcm)
        {
            if (qcm.EndTime != DateTime.MinValue)
            {
                qcm.EndTime = qcm.EndTime.AddDays(1);
            }

            if (qcm.Subject != null && qcm.Subject != "18")
            {
                ///排除无效状态的
                if (qcm.AssignStatus != null && qcm.AssignStatus.Length == 0)
                {
                    qcm.AssignStatus = new int[] { (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
                }
                ///如果没设置类型，则只取 一对一和班组
                if (qcm.CategoryType != null && qcm.CategoryType.Length == 0)
                {
                    qcm.CategoryType = new string[] { ((int)CategoryType.OneToOne).ToString(), ((int)CategoryType.CalssGroup).ToString() };
                }
            }
        }

        public void GetCourseConditionAssign(AssignQCM criteriaQCM)
        {
            if (criteriaQCM.EndTime != DateTime.MinValue)
            {
                criteriaQCM.EndTime = criteriaQCM.EndTime.AddDays(1);
            }
            ///排除无效状态的
            if (criteriaQCM.AssignStatus != null && criteriaQCM.AssignStatus.Length == 0)
            {
                criteriaQCM.AssignStatus = new int[] { (int)AssignStatusDefine.Assigned, (int)AssignStatusDefine.Exception, (int)AssignStatusDefine.Finished };
            }
            ///如果没设置类型，则只取 一对一和班组
            if (criteriaQCM.CategoryType != null && criteriaQCM.CategoryType.Length == 0)
            {
                criteriaQCM.CategoryType = new string[] { ((int)CategoryType.OneToOne).ToString(), ((int)CategoryType.CalssGroup).ToString() };
            }
        }

        public void GetProductCategoryType(Dictionary<string, IEnumerable<BaseConstantEntity>> dic)
        {
            ///处理查询条件   
            string key = "c_codE_ABBR_Product_CategoryType";
            foreach (var v in dic)
            {
                if (v.Key.ToLower() == key.ToLower())
                {
                    key = v.Key;
                    System.Collections.Generic.List<BaseConstantEntity> ie = (System.Collections.Generic.List<BaseConstantEntity>)v.Value;
                    var cc = from c in ie
                             where (new[] { ((int)CategoryType.CalssGroup).ToString(), ((int)CategoryType.OneToOne).ToString() }).Contains(c.Key)
                             select c;
                    dic.Remove(key);
                    dic.Add(key, cc);
                    break;
                }
            }
        }

        public void GetCourseAssignStatus(Dictionary<string, IEnumerable<BaseConstantEntity>> dic)
        {
            string key = "C_CODE_ABBR_Course_AssignStatus";
            foreach (var v in dic)
            {
                if (v.Key.ToLower() == key.ToLower())
                {
                    key = v.Key;
                    System.Collections.Generic.List<BaseConstantEntity> ie = (System.Collections.Generic.List<BaseConstantEntity>)v.Value;
                    var cc = from c in ie
                             where (new[] { ((int)AssignStatusDefine.Exception).ToString(), ((int)AssignStatusDefine.Finished).ToString() }).Contains(c.Key)
                             select c;
                    dic.Remove(key);
                    dic.Add(key, cc);
                    break;
                }
            }
        }

    }

    public class AssignQMBase
    {
        /// 学区ID
        public string CampusID { get; set; }
        ///开始日期
        public DateTime StartTime { get; set; }
        ///结束日期
        public DateTime EndTime { get; set; }
        ///是否UTC时间
        public bool IsUTCTime { get; set; }
        ///年级
        public string Grade { get; set; }
        ///排课状态
        public string AssignStatus { get; set; }
        ///课时类型
        //public string AssignSource { get; set; }
        public string CategoryType { get; set; }

    }

    public class CreateAssignQRBase
    {
        /// <summary>
        /// 排课条件列表
        /// 已经有的排课条件
        /// </summary>
        public AssignConditionCollection AssignCondition
        {
            get;
            set;
        }

        public AssignSuperModel Assign { get; set; }

        public IDictionary<string, IEnumerable<BaseConstantEntity>> Dictionaries { get; set; }

        public CreateAssignQRBase()
        {
            Assign = new AssignSuperModel();
            Assign.CopyAllowed = true;
        }

    }






}