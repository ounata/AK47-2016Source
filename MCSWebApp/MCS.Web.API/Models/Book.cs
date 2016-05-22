using MCS.Library.Office.OpenXml.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCS.Web.API.Models
{
    [TableDescription("藏书", "A1")]
    public class Book
    {
        [TableColumnDescription("书名")]
        public string Name
        {
            get;
            set;
        }

        [NotTableColumn]
        public Double Price
        {
            get;
            set;
        }

        [TableColumnDescription("发行日期", Format = "yyyy-MM-dd")]
        public DateTime IssueDate
        {
            get;
            set;
        }
    }
}