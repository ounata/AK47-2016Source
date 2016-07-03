using MCS.Library.Core;
using MCS.Library.Office.OpenXml.Excel;
using MCS.Web.API.Models;
using MCS.Web.MVC.Library.ApiCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MCS.Web.MVC.Library.ModelBinder;
using MCS.Web.MVC.Library.Providers;
using System.Text;
using MCS.Library.SOA.DataObjects;
using MCS.Web.MVC.Library.Models;

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

        [HttpPost]
        public HttpResponseMessage ExportAllBooksPost([ModelBinder(typeof(FormBinder))] Person p)
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

        [HttpPost]
        public void UploadExcel(HttpRequestMessage request)
        {
            request.ProcessFileUpload(pfArguments =>
            {
                Random random = new Random((int)DateTime.Now.Ticks);

                WorkBook workBook = WorkBook.Load(pfArguments.UploadedStream);

                workBook.Sheets.Any.IsNotNull(sheet => sheet.Tables.FirstOrDefault().IsNotNull(table =>
                  {
                      DataTable dt = table.AsDataTable();
                      StringBuilder errors = new StringBuilder();

                      int errorCount = 0;

                      pfArguments.Progress.MaxStep = dt.Rows.Count;

                      foreach (DataRow row in dt.Rows)
                      {
                          try
                          {
                              //假装执行花费100ms
                              Thread.Sleep(100);

                              //模拟出错的场景
                              (random.Next(10) > 7).TrueThrow("处理第{0}行数据出错", pfArguments.Progress.CurrentStep + 1);

                              //这里可以从DataRow转成对象。ORMapping.DataRowToObject

                              //只要有数据处理成功，就标记数据改变了
                              pfArguments.ProcessResult.DataChanged = true;
                          }
                          catch (System.Exception ex)
                          {
                              errorCount += 1;

                              //只要有数据错误，就不要关闭窗口，由用户来查看日志
                              pfArguments.ProcessResult.CloseWindow = false;

                              if (errors.Length > 0)
                                  errors.Append("\n");

                              errors.Append(ex.Message);
                          }
                          finally
                          {
                              pfArguments.Progress.CurrentStep += 1;
                              pfArguments.Progress.Response(string.Format("已经处理了{0}行数据", pfArguments.Progress.CurrentStep));
                          }
                      }

                      pfArguments.ProcessResult.ProcessLog = string.Format("总共处理了{0}行数据，其中{1}行出错", dt.Rows.Count, errorCount);
                      pfArguments.ProcessResult.Error = errors.ToString();
                  }));

                return true;
            });
        }

        [HttpPost]
        public HttpResponseMessage DownloadMaterial([ModelBinder(typeof(FormBinder))] MaterialModel material)
        {
            return material.ProcessMaterialDownload();
        }

       

        [HttpPost]
        public void UploadMaterialWriteDB(MaterialModelCollection materialCollection)
        {
            var c = materialCollection;
            MaterialModelHelper helper = MaterialModelHelper.GetInstance("DataAccessTest");
            helper.Update(c);

           
        }

        [HttpPost]
        public MaterialModelCollection UploadMaterial(HttpRequestMessage request)
        {

            
            return request.ProcessMaterialUpload();
        }

       

        
    }

    public class Person
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}