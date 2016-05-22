using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.WebAPI.Orders.ViewModels.ClassGroup
{
    public class EditClassLessonesModel
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public string ClassID { get; set; }

        /// <summary>
        /// 修改课次
        /// </summary>
        public string LessonID { get; set; }

        /// <summary>
        /// 周几上课
        /// </summary>
        public List<int> DayOfWeeks { get; set; }

        /// <summary>
        /// 上课开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        private List<DateTime> _startTimeList = null;

        /// <summary>
        /// 开始时间集合
        /// </summary>
        public List<DateTime> StartTimeList(int length)
        {
            if (_startTimeList == null && length > 0 && DayOfWeeks.Count>0)
            {
                for (int i = 0; i < DayOfWeeks.Count; i++)
                {
                    if (DayOfWeeks[i] == 7)
                        DayOfWeeks[i] = 0;
                }

                _startTimeList = new List<DateTime>();
                DateTime st = StartTime;
                bool flag = true;
                int c = 0;
                while (flag)
                {
                    if (c >= length)
                        break;
                    if (DayOfWeeks.Contains((int)st.DayOfWeek))
                    {
                        _startTimeList.Add(st);
                        st = st.AddDays(1);
                        c = c + 1;
                    }
                    else
                        st = st.AddDays(1);
                }
            }
            return _startTimeList;
        }

    }
}