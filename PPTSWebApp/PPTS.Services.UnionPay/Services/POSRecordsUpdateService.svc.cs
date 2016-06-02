using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PPTS.Contracts.UnionPay.Operations;
using System.ServiceModel.Web;
using PPTS.Services.UnionPay.ProcessCSVFile;
using PPTS.Data.Customers.Entities;
using System.Reflection;
using PPTS.Services.UnionPay.Model;
using PPTS.Services.UnionPay.Description;
using MCS.Library.Data.DataObjects;
using PPTS.Data.Customers.Adapters;
using PPTS.Services.UnionPay.Extensions;
using MCS.Library.WcfExtensions;

namespace PPTS.Services.UnionPay.Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“POSRecordsUpdateService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 POSRecordsUpdateService.svc 或 POSRecordsUpdateService.svc.cs，然后开始调试。
    public class POSRecordsUpdateService : IPOSRecordsUdateService
    {
        [WfJsonFormatter]
        [WebInvoke(Method = "POST",RequestFormat = WebMessageFormat.Json)]
        public void UpdatePOSRecords(string strConfigPath)
        {
            //
            string[][] ary = AnalyzeCSVFile.ReadFile(strConfigPath);
            POSRecordModel recordModel = new POSRecordModel();
            POSRecordCollection recordCollection = new POSRecordCollection();
            string[] aryTitle = ary[0];
                        
            for(int i=1;i<ary.Length;i++)
            {
                StatementModel model = new StatementModel();
                SetValueStatementModel(model, ary[i]);
                POSRecord record = new POSRecord() {
                    TransactionDate = model.TransactionTime.Date,
                    SettlementDate = model.LiquidationDate,
                    TransactionTimeValue = model.TransactionTime.ToString(),
                    TransactionTime = model.TransactionTime,
                    TransactionID = model.RefNum,
                    CardNum = model.CardNumber,
                    MerchantID = model.MerchantNumber,
                    POSID = model.TerminalNo,
                    Money = model.Amount,
                    FromType = UnionPaySourceType.Async.GetHashCode().ToString()
                };
                recordCollection.Add(record);
            }
            recordModel.POSRecordCollection = recordCollection;

            recordModel.UpdatePOSRecord();
        }
        private void SetValueStatementModel(StatementModel model, string[] aryDataLine)
        {
            Type type = model.GetType();
            PropertyInfo[] propertys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo pro in propertys)
            {
                DescriptionAttribute attribute = pro.GetCustomAttribute<DescriptionAttribute>();
                if (attribute != null)
                {
                    pro.SetValue(model, aryDataLine[attribute.Index].ToSelfType(pro.PropertyType));
                }
            }
        }
        
    }
}
