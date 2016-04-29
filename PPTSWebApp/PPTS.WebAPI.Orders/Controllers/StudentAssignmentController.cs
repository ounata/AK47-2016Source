using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MCS.Library.Data;
using PPTS.Data.Orders.Entities;
using PPTS.Data.Orders;
using PPTS.Data.Orders.DataSources;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Common.Adapters;
using PPTS.WebAPI.Orders.ViewModels.CustomerSearchs;
using PPTS.WebAPI.Orders.ViewModels.AssignConditions;
using PPTS.WebAPI.Orders.Executors;

namespace PPTS.WebAPI.Orders.Controllers
{
    public class StudentAssignmentController : ApiController
    {
        #region api/studentassignment/getAllStudentAssignment
        /// <summary>
        /// 获取学员待排课列表
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public CustomerSearchQueryResult GetAllStudentAssignment(CustomerSearchQueryCriteriaModel criteria)
        {
            ///流程需要赋值
            //criteria.CampusID = string.Empty;  操作人所属校区ID
            //criteria.CampusID = "A6F08B2E-2C0B-4A00-A41C-CD997941535D";
            CustomerSearchQueryResult result = new CustomerSearchQueryResult
            {
                QueryResult = GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy),
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(CustomerSearch))
            };

            return result;
        }
        /// <summary>
        /// 获取学员待排课列表，分页获取
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        [HttpPost]
        public PagedQueryResult<CustomerSearch, CustomerSearchCollection> GetPagedStudentAssignment(CustomerSearchQueryCriteriaModel criteria)
        {
            ///流程需要赋值
            //criteria.CampusID = string.Empty;  操作人所属校区ID
            return GenericSearchDataSource<CustomerSearch, CustomerSearchCollection>.Instance.Query(criteria.PageParams, criteria, criteria.OrderBy);
        }

        #endregion

        #region api/studentassignment/createAssignCondition
        /// <summary>
        /// 初始化排课数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public AssignConditionQueryResult GetAssignCondition(AssignConditionQueryModel queryModel)
        {
            ///学员ID
            string customerID = queryModel.CustomerID;
            ///流程需要赋值
            string operaterCampusID = string.Empty;
            
            AssignConditionCollection acc = AssignConditionAdapter.Instance.LoadCollection(operaterCampusID, customerID);
            acc.Insert(0, new AssignCondition() { ConditionID = "-1", ConditionName = "新建" });

            //IList<AssignSuper> avm = AssignsAdapter.Instance.LoadAssignSuper(operaterCampusID, customerID);


            OrderItemViewCollection avm = OrderItemViewAdapter.Instance.LoadCollection(operaterCampusID, customerID);
            ///从服务中获取学员指定的教师列表
            IList<TeacherModel> teacher = GetTearchBySubject(customerID);
            ///返回结果
            AssignConditionQueryResult result = new AssignConditionQueryResult()
            {
                AssignExtension = avm,
                AssignCondition = acc,
                Teacher = teacher,
                Dictionaries = ConstantAdapter.Instance.GetSimpleEntitiesByCategories(typeof(Assign))
            };
            return result;
        }
        /// <summary>
        /// 保存排课条件
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost]
        public void CreateAssignCondition(AssignExtension ae)
        {
            AssignAddExecutor ac = new AssignAddExecutor(ae);
            ac.Execute();
        }
        /// <summary>
        /// 根据学员ID和科目ID获取对应的教师列表
        /// 该列表数据需要从数据服务提供
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IList<TeacherModel> GetTearchBySubject(string customerID)
        {
            var teacheres = new List<TeacherModel>();

            teacheres.Add(new TeacherModel()
            {
                CustomerID = customerID,
                Subject = "2",
                TeacherID = "10001002",
                TeacherName = "蓝色天空2",
                Grade = "32",
                TeacherJobID = ""
            });

            teacheres.Add(new TeacherModel()
            {
                CustomerID = customerID,
                Subject = "2",
                TeacherID = "10002002",
                TeacherName = "珠穆朗玛2",
                Grade = "32",
                TeacherJobID = ""
            });

            teacheres.Add(new TeacherModel()
            {
                CustomerID = customerID,
                Subject = "7",
                TeacherID = "10001",
                TeacherName = "蓝色天空",
                Grade = "32",
                TeacherJobID = ""
            });

            teacheres.Add(new TeacherModel()
            {
                CustomerID = customerID,
                Subject = "7",
                TeacherID = "10002",
                TeacherName = "珠穆朗玛",
                Grade = "32",
                TeacherJobID = ""
            });


            return teacheres;

        }
        #endregion

        private static List<Schedule> AllScheduleData = new List<Schedule>();

        [HttpPost]
        public DataResult EditStudentCourse(ScheduleSimpleSearchCriteria criteria)
        {
            DataResult result = new DataResult()
            {
                Data = new
                {
                    List = SimpleSearchScheduleList(criteria)
                }
            };

            return result;
        }
        /// <summary>
        /// 复制排课
        /// </summary>
        public object CopyAssign(AssignConditionQueryModel queryModel)
        {
         
            object obj = new object();
            return obj;
        }
        /// <summary>
        /// 复制排课
        /// </summary>
        /// <param name="result"></param>
        public void CopyAssign(AssignCopyModel model)
        {
            AssignCopyExecutor ace = new AssignCopyExecutor(model);
            ace.Execute();
        }
        /// <summary>
        /// 调整课表
        /// </summary>
        /// <returns></returns>
        public object ResetAssign()
        {
            object obj = new object();
            return obj;
        }
        /// <summary>
        /// 调整课表
        /// </summary>
        /// <param name="model"></param>
        public void ResetAssign(AssignCollection model)
        {
            PPTS.Data.Orders.Executors.PPTSEditAssignExecutor executor = new Data.Orders.Executors.PPTSEditAssignExecutor("ResetAssign");
            executor.AssignCollection = model;
            executor.Execute();
        }
        /// <summary>
        /// 取消排课
        /// </summary>
        /// <param name="model"></param>
        public void CancelAssign(AssignCollection model)
        {
            AssignCancelExecutor executor = new AssignCancelExecutor(model);
            executor.Execute();

        }


        public List<Schedule> SimpleSearchScheduleList(ScheduleSimpleSearchCriteria criteria)
        {
            CreateAllScheduleData();
            var query = AllScheduleData.Where(a => a.start >= criteria.Start && a.end <= criteria.End);
            if (criteria.Teachers != null && criteria.Teachers.Count > 0)
            {
                foreach (Teacher teacher in criteria.Teachers)
                {
                    query = query.Where(a => a.title.Contains(teacher.TeacherName));
                }
            }
            return query.ToList();
        }
        public class DataResult
        {
            /// <summary>
            /// 0为成功，其他为失败编码
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 成功时为成功信息，错误时为失败消息
            /// </summary>
            public string Message { get; set; }
            public dynamic Data { get; set; }

            public DataResult() { }
            public DataResult(dynamic data)
            {
                this.Code = 0;
                this.Data = data;
            }
            public DataResult(int code, string message)
            {
                this.Code = code;
                this.Message = message;
            }
            public DataResult(int code, string message, dynamic data)
            {
                this.Code = code;
                this.Message = message;
                this.Data = data;
            }
        }
        private static void CreateAllScheduleData()
        {
            if (AllScheduleData.Count == 0)
            {
              
                string SPACE = " ";

                string[] courses = new string[] { "语文", "数学", "英语", "物理", "生物", "历史" };
                string[] colors = new string[] { "#82af6f", "#d15b47", "#9585bf", "#fee188", "#d6487e", "#3a87ad" };
                string[] textColors = new string[] { "#fff", "#fff", "#fff", "rgb(153, 102, 51)", "#fff", "#fff" };
                string[] allNames = new string[] { "孔苇", "冯添桂", "伍兆斌", "方艾健", "米希雨", "王瑶伶", "成萍娴", "余卓超" };
                string[] allStatuss = new string[] { "排定", "已上" };

                int[][] times = new int[][]
                {
                    new int[] {6, 30, 7, 0},
                    new int[] {8, 0, 9, 0},
                    new int[] {11, 0, 12, 0},
                    new int[] {13, 0, 13, 30},
                    new int[] {16, 0, 16, 30},
                    new int[] {18, 0, 19, 0}
                };

                Random rnd = new Random();
                for (DateTime dtStart = DateTime.Now; dtStart < DateTime.Now.AddDays(3); dtStart = dtStart.AddDays(1))
                {
                    int count = rnd.Next(times.Length);
                    List<int> indexList = GetRandomIndexList(count, times.Length, rnd);
                    for (int i = 0; i < indexList.Count; i++)
                    {
                        int[] time = times[indexList[i]];
                        int courseIndex = rnd.Next(courses.Length);
                        Schedule schedule = new Schedule()
                        {
                            id = Guid.NewGuid(),
                            title = "高三" + courses[courseIndex] + SPACE + allNames[rnd.Next(allNames.Length)],// + BR + allStatuss[rnd.Next(allStatuss.Length)],
                            allDay = false,
                            start = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, time[0], time[1], 0),
                            end = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, time[2], time[3], 0)
                        };
                        schedule.startText = schedule.start.ToString("HH:mm");
                        schedule.endText = schedule.end.ToString("HH:mm");
                        schedule.duration = (schedule.start - DateTime.Now).TotalDays;
                        if (schedule.start < DateTime.Now)
                        {
                            schedule.status = "已上";
                        }
                        else
                        {
                            schedule.status = "排定";
                        }

                        AllScheduleData.Add(schedule);
                    }
                }
            }
        }
        private static List<int> GetRandomIndexList(int count, int maxCount, Random rnd)
        {
            List<int> list = new List<int>();
            int mycount = 0;
            while (mycount < count)
            {
                int number = rnd.Next(maxCount);
                if (!list.Contains(number))
                {
                    list.Add(number);
                    mycount++;
                }
            }
            return list;
        }

        public class ScheduleSimpleSearchCriteria
        {
            //上课状态
            public string Status { get; set; }
            //上课年级
            public string Grade { get; set; }
            //上课教师
            public List<Teacher> Teachers { get; set; }
            public PagedParam PagedParam { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        public class Schedule
        {
            public Guid id { get; set; }
            //标题
            public string title { get; set; }
            //开始时间
            public DateTime start { get; set; }
            //结束时间
            public DateTime end { get; set; }
            public string startText { get; set; }
            public string endText { get; set; }
            //颜色
            public string color { get; set; }
            public string textColor { get; set; }
            //是否为全天事件
            public bool allDay { get; set; }
            public string status { get; set; }
            public double duration { get; set; }
        }
        public class Teacher
        {
            public Guid TeacherId { get; set; }
            public string TeacherCode { get; set; }
            public string TeacherName { get; set; }
            public string text { get; set; }
            public Guid DepartmentId { get; set; }
            public string DepartmentName { get; set; }
        }
        public class PagedParam
        {
            public int TotalCount { get; set; }
            public int Page { get; set; }
            public int Limit { get; set; }
            public int PageCount { get; set; }
            public string Message { get; set; }
            public PagedParam(int page, int limit, int totalCount)
            {
                this.Page = page;
                this.Limit = limit;
                this.TotalCount = totalCount;

                int a = totalCount / limit;
                int b = totalCount % limit;
                if (b == 0) this.PageCount = a;
                else this.PageCount = a + 1;

                this.Message = "共" + totalCount.ToString() + "条数据，当前显示" + ((this.Page - 1) * this.Limit + 1).ToString() + "到" + Math.Min(this.Page * this.Limit, this.TotalCount).ToString() + "条";
            }
            public PagedParam()
            {
                this.Page = 1;
                this.Limit = 10;
            }
        }
    }
}
