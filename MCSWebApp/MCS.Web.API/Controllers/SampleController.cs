using MCS.Library.Core;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Web.API.Models;
using MCS.Web.MVC.Library.ApiCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace MCS.Web.API.Controllers
{
    /*
        http://localhost/MCSWebApp/MCS.Web.API/api/Sample/ExportAllBooks
        http://localhost/MCSWebApp/MCS.Web.API/api/Sample/ExportAllBooksWithCustomColumns
        http://localhost/MCSWebApp/MCS.Web.API/api/Sample/ExportAllBooksCallback
     */
    /// <summary>
    /// 可以尝试链接
    /// </summary>
    public class SampleController : ApiController
    {
        /// <summary>
        /// 演示在对象属性上定义导出的列信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage ExportAllBooks()
        {
            List<Book> books = DataHelper.PrepareBooks();

            WorkBook wb = WorkBook.CreateNew();

            WorkSheet sheet = wb.Sheets["sheet1"];

            sheet.LoadFromCollection(books);

            return wb.ToResponseMessage("所有藏书（无价格）.xlsx");
        }

        [HttpGet]
        public HttpResponseMessage ExportAllBooksWithCustomColumns()
        {
            List<Book> books = DataHelper.PrepareBooks();

            WorkBook wb = WorkBook.CreateNew();

            WorkSheet sheet = wb.Sheets["sheet1"];

            TableDescription tableDesp = new TableDescription("Books");

            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("书名", typeof(string))) { PropertyName = "Name" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("价格", typeof(double))) { PropertyName = "Price", Format = "#,##0.00" });

            sheet.LoadFromCollection(books, tableDesp, null);

            return wb.ToResponseMessage("所有藏书（无日期）.xlsx");
        }

        [HttpGet]
        public HttpResponseMessage ExportAllBooksCallback()
        {
            List<Book> books = DataHelper.PrepareBooks();

            WorkBook wb = WorkBook.CreateNew();

            WorkSheet sheet = wb.Sheets["sheet1"];

            TableDescription tableDesp = new TableDescription("Books");

            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("书名", typeof(string))) { PropertyName = "Name" });
            tableDesp.AllColumns.Add(new TableColumnDescription(new DataColumn("价格", typeof(double))) { PropertyName = "Price", Format = "#,##0.00" });

            sheet.LoadFromCollection(books, tableDesp, (cell, param) =>
            {
                if (param.ColumnDescription.PropertyName == "Price")
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                cell.Value = param.ColumnDescription.FormatValue(param.PropertyValue);
            });

            return wb.ToResponseMessage("所有藏书.xlsx");
        }
    }
}