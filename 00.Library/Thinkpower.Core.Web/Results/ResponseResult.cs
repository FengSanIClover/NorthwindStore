using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Thinkpower.Core.Http;
using System.Linq;

namespace Thinkpower.Core.Web
{
    public class ResponseResult : IHttpActionResult
    {
        private string returnCode;
        private string message;
        private HttpRequestMessage request;

        public ResponseResult(string returnCode, string message,
            HttpRequestMessage request)
        {
            this.returnCode = returnCode;
            this.message = message;
            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var baseResponse = new Response();
            baseResponse.ReturnCode = this.returnCode;
            baseResponse.Message = this.message;

            var json = JsonConvert.SerializeObject(baseResponse,
                new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new JsonStringNullToEmpty() });

            var response = new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                RequestMessage = this.request
            };

            return Task.FromResult(response);
        }
    }

    public class ResponseResult<T> : IHttpActionResult
    {
        private T content;
        private string returnCode;
        private string message;
        private HttpRequestMessage request;

        public ResponseResult(string returnCode, string message, T content,
            HttpRequestMessage request)
        {
            this.returnCode = returnCode;
            this.message = message;
            this.content = content;

            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var baseResponse = new Response<T>
            {
                ReturnCode = this.returnCode,
                Message = this.message,
                Result = this.content
            };

            var json = JsonConvert.SerializeObject(baseResponse,
                new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new JsonStringNullToEmpty() });

            var response = new HttpResponseMessage()
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                RequestMessage = this.request
            };

            return Task.FromResult(response);
        }
    }

    // 字段值為 null 時，序列化或反序列化成其他值

    /// <summary>
    /// 實作 Newtonsoft.Json 的 DefaultContractResolver
    /// </summary>
    public class JsonStringNullToEmpty : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        public JsonStringNullToEmpty() { }

        protected override System.Collections.Generic.IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(
            System.Type oType, Newtonsoft.Json.MemberSerialization oMS)
        {
            return oType.GetProperties().Select(oP =>
            {
                // base.CreateProperty() 取得 JsonProperty
                var oJP = base.CreateProperty(oP, oMS); 
                oJP.ValueProvider = new JsonStringNullToEmptyValueProvider(oP);
                return oJP;
            }).ToList();
        }
    }

    /// <summary>
    /// 實作 Newtonsoft.Json 的 IValueProvider
    /// </summary>
    internal class JsonStringNullToEmptyValueProvider : Newtonsoft.Json.Serialization.IValueProvider
    {
        System.Reflection.PropertyInfo _oMemberInfo;

        // 建構子（將成員資訊帶入成為內部變數）
        public JsonStringNullToEmptyValueProvider(System.Reflection.PropertyInfo oMI) { _oMemberInfo = oMI; }

        // 實作 IValueProvider 介面的寫入動作
        public void SetValue(object oTarget, object oValue) { _oMemberInfo.SetValue(oTarget, oValue); }

        // 實作 IValueProvider 介面的寫入動作
        public object GetValue(object oTarget)
        {
            // 設定回傳變數
            object oResult = _oMemberInfo.GetValue(oTarget);
            // 若成員為字串型態，就處理他
            if (_oMemberInfo.PropertyType == typeof(System.String) && oResult == null) oResult = string.Empty;
          
            // 回傳結果
            return oResult;
        }
    }
}