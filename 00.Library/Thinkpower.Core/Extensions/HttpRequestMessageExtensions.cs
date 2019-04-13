using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Linq;

namespace Thinkpower.Core.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static string FetchHeader(this HttpRequestMessage request, string headerKey)
        {
            if (request.Headers.Contains(headerKey))
                return request.Headers.Single(o => o.Key == headerKey).Value.FirstOrDefault();
            else
                return null;
        }

        public static string FetchHeader(this HttpResponseMessage response, string headerKey)
        {
            if (response.Headers.Contains(headerKey))
                return response.Headers.Single(o => o.Key == headerKey).Value.FirstOrDefault();
            else
                return null;
        }

        /// <summary>
        /// Get Token from Authorization Header(Bearer)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool TryRetrieveToken(this HttpRequestMessage request, out string token)
        {
            token = null;

            var authorization = request.Headers.Authorization;
            if (authorization != null && authorization.Scheme == "Bearer")
            {
                token = authorization.Parameter;
                return true;
            }

            return false;
        }
    }
}
