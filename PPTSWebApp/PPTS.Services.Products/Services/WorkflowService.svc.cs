using MCS.Library.Net.SNTP;
using MCS.Library.WcfExtensions;
using PPTS.Contracts.Products.Operations;
using PPTS.Data.Common.Security;
using PPTS.Data.Products.Adapters;
using PPTS.Data.Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MCS.Library.Core;

namespace PPTS.Services.Products.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WorkflowService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WorkflowService.svc 或 WorkflowService.svc.cs，然后开始调试。
    public class WorkflowService : IWorkflowService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AddProductProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            var model = ProductAdapter.Instance.Load(CurrentResourceID);

            model.NullCheck("CurrentResourceID:model");
            model.ProductStatus = Data.Products.ProductStatus.Refused;
            model.ModifierID = CurrentUserID;
            model.ModifierName = CurrentUserName;

            ProductAdapter.Instance.Update(model);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void AddProductProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            var model = ProductAdapter.Instance.Load(CurrentResourceID);

            model.NullCheck("CurrentResourceID:model");
            model.ProductStatus = Data.Products.ProductStatus.Approved;
            model.ModifierID = CurrentUserID;
            model.ModifierName = CurrentUserName;

            ProductAdapter.Instance.Update(model);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DiscountProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            Discount d = DiscountAdapter.Instance.Load(builder => builder.AppendItem("DiscountID", CurrentResourceID)).FirstOrDefault();
            d.DiscountStatus = Data.Products.DiscountStatusDefine.Refused;
            d.ApproverID = CurrentUserID;
            d.ApproverName = CurrentUserName;
            //d.ApproverJobID = CreateJob.ID;
            //d.ApproverJobName = CreateJob.Name;
            d.ApproveTime = SNTPClient.AdjustedTime;
            DiscountAdapter.Instance.Update(d);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void DiscountProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            //折扣表
            Discount d = DiscountAdapter.Instance.Load(builder => builder.AppendItem("DiscountID", CurrentResourceID)).FirstOrDefault();
            d.DiscountStatus = Data.Products.DiscountStatusDefine.Approved;
            d.ApproverID = CurrentUserID;
            d.ApproverName = CurrentUserName;
            //d.ApproverJobID = CreateJob.ID;
            //d.ApproverJobName = CreateJob.Name;
            d.ApproveTime = SNTPClient.AdjustedTime;
            DiscountAdapter.Instance.Update(d);

            //折扣表  校区 关系
            DiscountPermissionsApplieCollection dpaList = DiscountPermissionsApplyAdapter.Instance.Load(builder => builder.AppendItem("DiscountID", d.DiscountID));
            //关闭
            StringBuilder sb = new StringBuilder();
            foreach (var item in dpaList)
            {
                sb.AppendFormat(",'{0}'", item.CampusID);
            }
            DiscountPermissionCollection dpc = DiscountPermissionAdapter.Instance.Load(builder => builder.AppendItem("CampusID", "(" + sb.ToString().Substring(1) +")", "in", true));
            foreach (var item in dpc)
            {
                if (item.EndDate > d.StartDate)
                {
                    item.EndDate = d.StartDate;
                    DiscountPermissionAdapter.Instance.UpdateInContext(item);
                }
            }
            //放开
            foreach (var item in dpaList)
            {
                DiscountPermission dp = new DiscountPermission();
                dp.CampusID = item.CampusID;
                dp.DiscountID = item.DiscountID;
                dp.StartDate = d.StartDate;
                dp.FillCreator();
                DiscountPermissionAdapter.Instance.UpdateInContext(dp);
            }
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void PresentProcessCancelling(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            Present d = PresentAdapter.Instance.Load(builder => builder.AppendItem("PresentID", CurrentResourceID)).FirstOrDefault();
            d.PresentStatus = Data.Products.PresentStatusDefine.Refused;
            d.ApproverID = CurrentUserID;
            d.ApproverName = CurrentUserName;
            //d.ApproverJobID = CreateJob.ID;
            //d.ApproverJobName = CreateJob.Name;
            d.ApproveTime = SNTPClient.AdjustedTime;
            PresentAdapter.Instance.Update(d);
        }

        [WfJsonFormatter]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void PresentProcessCompleting(string CurrentResourceID, string CurrentUserID, string CurrentUserName)
        {
            //买赠表
            Present d = PresentAdapter.Instance.Load(builder => builder.AppendItem("PresentID", CurrentResourceID)).FirstOrDefault();
            d.PresentStatus = Data.Products.PresentStatusDefine.Approved;
            d.ApproverID = CurrentUserID;
            d.ApproverName = CurrentUserName;
            //d.ApproverJobID = CreateJob.ID;
            //d.ApproverJobName = CreateJob.Name;
            d.ApproveTime = SNTPClient.AdjustedTime;
            PresentAdapter.Instance.UpdateInContext(d);

            //买赠表  校区 关系
            PresentPermissionsApplieCollection dpaList = PresentPermissionsApplyAdapter.Instance.Load(builder => builder.AppendItem("PresentID", d.PresentID));
            //关闭
            StringBuilder sb = new StringBuilder();
            foreach (var item in dpaList)
            {
                sb.AppendFormat(",'{0}'", item.CampusID);
            }
            PresentPermissionCollection dpc = PresentPermissionAdapter.Instance.Load(builder => builder.AppendItem("CampusID","(" + sb.ToString().Substring(1)+")", "in", true));
            foreach (var item in dpc)
            {
                if (item.EndDate > d.StartDate)
                {
                    item.EndDate = d.StartDate;
                    PresentPermissionAdapter.Instance.UpdateInContext(item);
                }
            }
            //放开
            foreach (var item in dpaList)
            {
                PresentPermission dp = new PresentPermission();
                dp.CampusID = item.CampusID;
                dp.PresentID = item.PresentID;
                dp.StartDate = d.StartDate;
                dp.FillCreator();
                PresentPermissionAdapter.Instance.UpdateInContext(dp);
            }
        }
    }
}
