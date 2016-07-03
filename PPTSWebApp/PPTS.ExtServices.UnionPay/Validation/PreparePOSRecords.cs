
using MCS.Library.Core;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.UnionPay.Models.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPTS.ExtServices.UnionPay.Validation
{
    public class PreparePOSRecords
    {
        /// <summary>
        /// 解析参数，返回POSRecordsModel对象，完成录入数据准备
        /// </summary>
        /// <param name="model">webAPI参数</param>
        /// <param name="transType">刷卡类型1--银联，4--通联</param>
        /// <param name="paySorType">1--同步（实时），2--异步（对账单）</param>
        /// <returns></returns>
        public POSRecordsModel AnalyzeParameters(RequestModel model, POSTransactionType transType, PaySourceType paySorType)
        {
            POSRecordsModel recordsModel = new POSRecordsModel();

            //如果是同步接口则结算数据集合ListStatementModel数量是一个
            if (PaySourceType.Sync == paySorType && (model.ListStatementModel.Count <= 0 || model.ListStatementModel.Count > 1))
            {
                true.TrueThrow("参数结算数据集合数量必须是一个");
            }
            recordsModel.RequestTime = model.RequestTime;
            

            //验证数据的合法性，如果有一个数据不合法则全部废弃。
            string strErrorMessage = string.Empty;
            model.ListStatementModel.ForEach((StatementModel statementModel)=> {
                ValidateExecutor vExecutor = new ValidateExecutor();
                vExecutor.GetValidateResult(statementModel, out strErrorMessage);
                if (!strErrorMessage.IsNullOrEmpty())
                {
                    true.TrueThrow(strErrorMessage);
                }
            });
            //遍历填充POSRecordCollection
            POSRecordCollection recordCollection = new POSRecordCollection();
            model.ListStatementModel.ForEach((StatementModel statementModel) => {
                POSRecord record = new POSRecord()
                {
                    TransactionDate = DateTime.Parse(statementModel.TransactionTime).Date,
                    SettlementDate = string.IsNullOrEmpty(statementModel.LiquidationDate) ? DateTime.MinValue : DateTime.Parse(statementModel.LiquidationDate),
                    TransactionTime = DateTime.Parse(statementModel.TransactionTime),
                    TransactionID = statementModel.RefNum,
                    CardNum = statementModel.CardNumber,
                    MerchantID = statementModel.MerchantNumber,
                    POSID = statementModel.TerminalNo,
                    Money = statementModel.Amount,
                    FromType = Convert.ToInt32(paySorType).ToString(),
                    TransactionTimeValue = statementModel.TransactionTime.ToString(),
                    TransactionType = Convert.ToInt32(transType).ToString()
                };
                recordCollection.Add(record);
            });
            recordsModel.POSRecordCollection = recordCollection;
            //根据接口来源类型，确定日志内容
            switch (paySorType)
            {
                case PaySourceType.Sync:
                    recordsModel.TransactionType = Convert.ToInt32(PaySourceType.Sync).ToString();
                    recordsModel.LogContent = recordsModel.RequestTime.ToString() + "_" + Convert.ToInt32(transType).ToString() + "_" + recordsModel.POSRecordCollection[0].TransactionID;
                    break;
                case PaySourceType.Async:
                    recordsModel.TransactionType = Convert.ToInt32(PaySourceType.Async).ToString();
                    recordsModel.LogContent = recordsModel.RequestTime.ToString() + "_" + Convert.ToInt32(transType).ToString();
                    break;
                default:
                    break;
            }

            return recordsModel;
        }
    }
}