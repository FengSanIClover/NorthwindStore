using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyLibrary
{
   public class MyHttpClient
    {  // Newtonsoft.Json 載入
        private string baseUrl;
        private int timeOut;

        public MyHttpClient() { }

        public MyHttpClient(string baseUrl, int timeOut = 60*1000)
        {
            this.baseUrl = baseUrl;
            this.timeOut = timeOut;
        }

        public T PostRequest<T>(string methodUrl)
        {
            var requestUrl = $"{this.baseUrl}/{methodUrl}";
            return this.Post<T>((client, token) => client.SendAsync
            (new HttpRequestMessage(HttpMethod.Post, requestUrl)
            ,token));
        }

        public T PostJsonRequest<T>(string methodUrl,object jsonObject)
        {
            var jsonstring = JsonConvert.SerializeObject(jsonObject);
            var requestUrl = $"{this.baseUrl}/{methodUrl}";
            return this.Post<T>((client, token) => client.PostAsync(
                requestUrl, new StringContent(jsonstring,Encoding.UTF8, "application/json"),token));
        }

        private T Post<T>(Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> func)
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri(this.baseUrl) };

            CancellationTokenSource cancelToken = new CancellationTokenSource(this.timeOut);
            HttpResponseMessage response = null;
            Exception ex = null;
            try
            {
                response = func(httpClient, cancelToken.Token)
                   .ContinueWith<HttpResponseMessage>(r =>
                   {
                       if (r.IsCompleted)
                           return r.Result;

                       if (r.IsCanceled)
                           ex = new TimeoutException(); // 理論上不會觸發
                       else if (r.Exception != null)
                           ex = r.Exception.InnerException;
                       return null;
                   }).Result;
            }
            catch (AggregateException e)
            {
                Exception error = e;
                do
                {
                    error = error.InnerException;
                    if (error is System.Threading.Tasks.TaskCanceledException)
                    {
                        ex = error;
                        break;
                    }

                } while (error != null);

                if (ex == null)
                    throw e;
                else
                    throw new TimeoutException(string.Empty, ex);
            }


            var responseJsonString =  response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(responseJsonString);
        }
    }
}
