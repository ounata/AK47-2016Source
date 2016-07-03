using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PPTS.ExtServices.UnionPay.Filters
{
    public class PassportAuthenticationFailureResult : IHttpActionResult
    {
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            response.ReasonPhrase = "token is error";
            
            response.Content = new StringContent("{Flag:0,ErrorMessage:\"Code:"+ Convert.ToInt32(response.StatusCode).ToString() + ",token is error\"}", System.Text.Encoding.UTF8, "text/json");
            return Task.FromResult(response);
        }
    }
}