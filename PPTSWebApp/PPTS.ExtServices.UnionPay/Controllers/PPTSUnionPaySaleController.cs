using System;
using System.Web.Http;

using MCS.Library.Core;
using PPTS.ExtServices.UnionPay.Models.Response;
using PPTS.ExtServices.UnionPay.Models.Statement;
using PPTS.ExtServices.UnionPay.Validation;
using PPTS.Data.Customers.Entities;
using PPTS.Data.Customers;
using System.Collections.Generic;
using PPTS.ExtServices.UnionPay.Filters;
using System.Configuration;

namespace PPTS.ExtServices.UnionPay.Controllers
{
    [POSAuthentication(AuthenPassport = "UnionPayKeyToken")]
    public class PPTSUnionPaySaleController : ApiController
    {
        /// <summary>
        /// 实时接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel PostStatement([FromBody]StatementModel model)
        {
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

                recordModel = prepareRecords.AnalyzeParameters(requestModel,POSTransactionType.UnionPay,PaySourceType.Sync);

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

                recordModel = prepareRecords.AnalyzeParameters(requestModel, POSTransactionType.UnionPay, PaySourceType.Async);

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
