using MCS.Library.Core;
using PPTS.Data.Customers;
using PPTS.Data.Customers.Entities;
using PPTS.ExtServices.UnionPay.Filters;
using PPTS.ExtServices.UnionPay.Models.Response;
using PPTS.ExtServices.UnionPay.Models.Statement;
using PPTS.ExtServices.UnionPay.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PPTS.ExtServices.UnionPay.Controllers
{
    [POSAuthentication(AuthenPassport = "AllInPayKeyToken")]
    public class PPTSAllInPaySaleController : ApiController
    {
        /// <summary>
        /// 通联实时
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel PostStatement([FromBody]StatementModel model)
        {
            //File.WriteAllText("D:\\a.txt", HttpContext.Current.Request.Headers["token"]);
            ResponseModel response = new ResponseModel();
            response.Flag = true;
            response.ErrorMessage = string.Empty;
            POSRecordsModel recordModel = new POSRecordsModel();

            try
            {
                //model.NullCheck("参数不能为空");
                //model.ListStatementModel.NullCheck("参数结算数据集合不能为空");
                model.NullCheck("参数不能为空");

                RequestModel requestModel = new RequestModel();
                requestModel.RequestTime = DateTime.Now;
                List<StatementModel> lstStatementModel = new List<StatementModel>();
                lstStatementModel.Add(model);
                requestModel.ListStatementModel = lstStatementModel;

                PreparePOSRecords prepareRecords = new PreparePOSRecords();

                recordModel = prepareRecords.AnalyzeParameters(requestModel, POSTransactionType.AllInPay, PaySourceType.Sync);

                //执行
                recordModel.UpdatePOSRecord();

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        /// <summary>
        /// 通联对账单接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel PostListStatement([FromBody]RequestModel model)
        {
            ResponseModel response = new ResponseModel();
            response.Flag = true;
            response.ErrorMessage = string.Empty;
            POSRecordsModel recordModel = new POSRecordsModel();

            try
            {
                model.NullCheck("参数不能为空");
                model.ListStatementModel.NullCheck("参数结算数据集合不能为空");

                RequestModel requestModel = new RequestModel();
                requestModel.RequestTime = DateTime.Now;
                requestModel.ListStatementModel = model.ListStatementModel;

                PreparePOSRecords prepareRecords = new PreparePOSRecords();

                recordModel = prepareRecords.AnalyzeParameters(requestModel, POSTransactionType.AllInPay, PaySourceType.Async);

                //执行
                recordModel.UpdatePOSRecord();

            }
            catch (Exception e)
            {
                response.Flag = false;
                response.ErrorMessage = e.Message;
            }

            return response;
        }
    }
}
