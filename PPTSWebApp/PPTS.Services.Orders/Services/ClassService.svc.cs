using MCS.Library.Data.Adapters;
using MCS.Library.Net.SNTP;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Orders.Operations;
using PPTS.Data.Common;
using PPTS.Data.Orders;
using PPTS.Data.Orders.Adapters;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PPTS.Services.Orders.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ClassService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 ClassService.svc 或 ClassService.svc.cs，然后开始调试。
    public class ClassService : IClassService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void ConfirmClassLesson(DateTime ConfirmTime)
        {
            DateTime closeDate = ConfigsCache.GetGlobalArgs().GetCurrentClosingAccountDate();

            //1.确认课时
            DbHelper.RunSPReturnDS("[OM].[p_ConfirmClassLesson]", ClassesAdapter.Instance.ConnectionName, ConfirmTime);
            //2.同步班级信息
            DbHelper.RunSPReturnDS("[OM].[p_SyncClassesInfo]", ClassesAdapter.Instance.ConnectionName, closeDate);
            //3.同步班级数量到产品
            string sqlText = "select ProductID from  [OM].[Classes] where EndTime >  DATEADD(mm,-6, GETUTCDATE())";
            DataSet ds = DbHelper.RunSqlReturnDS(sqlText, ClassesAdapter.Instance.ConnectionName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    SyncClassCountToProduct(item["ProductID"].ToString());
                }
            }
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void Job_ConfirmClassLesson() {
            DateTime dt = SNTPClient.AdjustedUtcTime.AddDays(-2).Date;
            ConfirmClassLesson(dt);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void SyncClassCountToProduct(string productID)
        {
            //1.获取数据
            DateTime closeDate = ConfigsCache.GetGlobalArgs().GetCurrentClosingAccountDate();

            string sqlText = string.Format(@"select [CampusID],[ProductID],count(1) [ClassCount],(sum(case when EndTime < '{1}' then 1 else 0 end)) [ValidClasses]
                                                from [OM].[Classes] 
                                                where ProductID = '{0}' and ClassStatus != {2}
                                                group by [CampusID],[ProductID]", productID, closeDate.ToString("yyyy-MM-dd hh:mm"), ((int)ClassStatusDefine.Deleted).ToString());
            DataSet ds = DbHelper.RunSqlReturnDS(sqlText, ClassesAdapter.Instance.ConnectionName);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                //2.清除
                ProductClassStatsAdapter.Instance.DeleteInContext(action => action.AppendItem("ProductID", productID));


                //3.插入数据
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ProductClassStatsAdapter.Instance.UpdateInContext(new ProductClassStat() { CampusID = row["CampusID"].ToString(), ProductID = row["ProductID"].ToString(), ClassCount = int.Parse(row["ClassCount"].ToString()), ValidClasses = int.Parse(row["ValidClasses"].ToString()) });
                }

                ProductClassStatsAdapter.Instance.GetDbContext().ExecuteNonQuerySqlInContext();
            }
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public  void Job_InitClassCountToProduct() {
            DateTime closeDate = ConfigsCache.GetGlobalArgs().GetCurrentClosingAccountDate();

            //同步班级信息
            DbHelper.RunSPReturnDS("[OM].[p_SyncClassesInfo]", ClassesAdapter.Instance.ConnectionName, closeDate,1);

            string sqlText = "select distinct ProductID from  [OM].[Classes] ";
            DataSet ds = DbHelper.RunSqlReturnDS(sqlText, ClassesAdapter.Instance.ConnectionName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    SyncClassCountToProduct(item["ProductID"].ToString());
                }
            }
        }


    }
}
