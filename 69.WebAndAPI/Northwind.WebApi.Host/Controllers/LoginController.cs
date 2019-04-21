using Adapters;
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
        public ActionResult Login(string id = null)
        {
            var result = this.authAdapter.SendRequest<Response<string>>("Test");

            return Json(result.Result);
        }
    }
}