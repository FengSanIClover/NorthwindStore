using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Thinkpower.Core.Web;

namespace Northwind.WebApi.Host.Controllers.Apis
{
    public class BaseApiController : ApiController
    {
        public ResponseResult Success(string message = "成功")
        {
            return new ResponseResult("0000", message, Request);
        }

        public ResponseResult<T> Success<T>(T content, string message = "成功")
        {
            return new ResponseResult<T>("0000", message,content, Request);
        }

        public ResponseResult<T> Success<T>(T content, string returnCode, string message = "成功")
        {
            return new ResponseResult<T>(returnCode, message, content, Request);
        }
    }
}
