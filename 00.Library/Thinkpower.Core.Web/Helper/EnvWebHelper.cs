using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Thinkpower.Core.Helper;

namespace Thinkpower.Core.Web.Helper
{
    public class EnvWebHelper
    {
        /// <summary>
        /// 取得用戶端 IP
        /// </summary>
        /// <returns>用戶端 IP</returns>
        public static string GetClientIP()
        {
            if (HttpContext.Current != null) return HttpContext.Current.Request.UserHostAddress;
            else return EnvHelper.GetServerIP();
        }

        /// <summary>
        /// 取得用戶端代理程式
        /// </summary>
        /// <returns>用戶端代理程式</returns>
        public static string GetUserAgent()
        {
            if (HttpContext.Current != null) return HttpContext.Current.Request.UserAgent;
            else return string.Empty;
        }
    }
}
