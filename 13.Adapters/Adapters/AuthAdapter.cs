using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinkpower.Core.Http;
using Newtonsoft.Json;

namespace Adapters
{
   public class AuthAdapter
    {
        private MyHttpClient httpclient;
        private string baseUrl = "http://localhost:1095/api/Auth";

        public AuthAdapter()
        {
            this.httpclient = new MyHttpClient(baseUrl);
        }

        public Response<T> SendRequest<T>(string actionName)
        {
            try
            {
                return this.httpclient.PostRequest<Response<T>>(actionName);
            }
            catch (Exception ex)
            {
                return new Response<T>(default(T), "9999", $"錯誤訊息:{ex.Message}");
            }
        }

        public Response<T> SendJsonRequest<T>(string actionName, object req)
        {
            try
            {
                return this.httpclient.PostJsonRequest<Response<T>>(actionName, req);
            }
            catch (Exception ex)
            {
                return new Response<T>(default(T), "9999", $"錯誤訊息:{ex.Message}");
            }
        }
    }
}
