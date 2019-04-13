using System;
using System.Collections.Generic;
using System.Text;

namespace Thinkpower.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static string InnerTypeName(this Exception ex)
        {
            if (ex.InnerException != null)
                return ex.InnerException.InnerTypeName();

            return ex.GetType().Name;
        }

        public static string GetFullMessage(this Exception ex)
        {
            if (ex.InnerException != null)
                return ex.Message + "-" + ex.InnerException.GetFullMessage();
            return ex.Message;
        }
    }
}
