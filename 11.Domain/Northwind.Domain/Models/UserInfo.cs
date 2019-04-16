using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Models
{
   public class UserInfo
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Token 到期時間
        /// </summary>
        public DateTime TokenExpiredOn { get; set; }
    }
}
