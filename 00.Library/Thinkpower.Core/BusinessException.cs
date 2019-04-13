using System;

namespace Thinkpower.Core
{
    public class BusinessException : Exception
    {
        public string ErrorMsg { get; set; }
        public string ErrorCode { get; set; }

        public BusinessException(string errorCode = "", string errorMsg = "")
        {
            this.ErrorMsg = errorMsg;
            this.ErrorCode = errorCode;
        }
    }
}