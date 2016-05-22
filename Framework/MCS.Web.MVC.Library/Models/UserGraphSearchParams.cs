using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Web.MVC.Library.Models
{
    public class UserGraphSearchParams : UserGraphTreeParams
    {
        private int _MaxCount = 15;

        /// <summary>
        /// 搜索文字
        /// </summary>
        public string SearchTerm
        {
            get;
            set;
        }

        /// <summary>
        /// 最大返回行数
        /// </summary>
        public int MaxCount
        {
            get
            {
                return this._MaxCount;
            }
            set
            {
                this._MaxCount = value;
            }
        }
    }
}
