using System;
using System.Web.Http;

using MCS.Library.Core;
using PPTS.ExtServices.UnionPay.Models.Response;
using PPTS.ExtServices.UnionPay.Models.Statement;
using PPTS.ExtServices.UnionPay.Validation;
using PPTS.Data.Customers.Entities;

namespace PPTS.ExtServices.UnionPay.Controllers
{
    public class PPTSUnionPaySaleController : ApiController
    {
        [Route("api/PPTSUnionPaySale/PostStatement")]
        [HttpPost]
        public ResponseModel PostStatement([FromBody]StatementModel model)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                string strErrorMessage = string.Empty;
                ValidateExecutor vExecutor = new ValidateExecutor();
                vExecutor.GetValidateResult(model, out strErrorMessage);
                if (!strErrorMessage.IsNullOrEmpty())
                {
                    true.TrueThrow(strErrorMessage);
                }
                else
                {
                    POSRecordsModel posRecordModel = new POSRecordsModel();
                    posRecordModel.PosRecord = new POSRecord()
                    {
                        TransactionDate = model.TransactionTime.Date,
                        SettlementDate = model.LiquidationDate,
                        TransactionTime = model.TransactionTime,
                        TransactionID = model.RefNum,
                        CardNum = model.CardNumber,
                        MerchantID = model.MerchantNumber,
                        POSID = model.TerminalNo,
                        Money = model.Amount,
                        FromType = UnionPaySourceType.Sync.GetHashCode().ToString(),
                        TransactionTimeValue = model.TransactionTime.ToString()
                    };

                    posRecordModel.UpdatePOSRecord();
                }

            }
            catch (Exception e)
            {
                response.Flag = "1";
                response.ErrorMessage = e.Message;
            }
            finally
            {
                if (response.Flag != "1")
                {
                    response.Flag = "0";
                    response.ErrorMessage = string.Empty;
                }
            }

            return response;
        }
        
       
    }
}
