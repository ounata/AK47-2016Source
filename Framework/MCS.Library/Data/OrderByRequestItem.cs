using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Data
{
    /// <summary>
    /// OrderBy的参数
    /// </summary>
    public class OrderByRequestItem : IOrderByRequestItem
    {
        /// <summary>
        /// 数据字段
        /// </summary>
        public string DataField
        {
            get;
            set;
        }

        /// <summary>
        /// 排序方向
        /// </summary>
        public FieldSortDirection SortDirection
        {
            get;
            set;
        }

        /// <summary>
        /// 从源复制
        /// </summary>
        /// <param name="source"></param>
        public void CopyFrom(IOrderByRequestItem source)
        {
            if (source != null)
            {
                this.DataField = source.DataField;
                this.SortDirection = source.SortDirection;
            }
        }
    }
}
