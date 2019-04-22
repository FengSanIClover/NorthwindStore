using Adapters;
using Northwind.Domain.Models;
using Northwind.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinkpower.Core.Http;

namespace Northwind.WebApi.Host.Controllers
{
    public class LoginController : Controller
    {
        AuthAdapter authAdapter = new AuthAdapter();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Accounts accountReq)
        {
            accountReq.UserAccount = "1";
            accountReq.UserPassword = "1";
            var result = this.authAdapter.SendJsonRequest<UserInfo>("login",accountReq);

            return Json(result.Result);
        }
    }
}