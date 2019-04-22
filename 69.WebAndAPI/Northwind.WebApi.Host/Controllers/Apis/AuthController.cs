using Northwind.Domain.Models;
using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Northwind.WebApi.Host.Controllers.Apis
{
    [RoutePrefix("api/Auth")]
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class AuthController : BaseApiController
    { 
        private IAuthorizes authorizeService;

        public AuthController(IAuthorizes authorizeService)
        {
            this.authorizeService = authorizeService;
        }

        [Route("Test")]
        [HttpPost]
        public IHttpActionResult Test()
        {
            return Success("OK");
        }

        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> Authenticate(Accounts loginReq)
        {
            var result = await this.authorizeService.CreateUserInfo(loginReq.UserAccount, loginReq.UserPassword);
            return  Success(result);
        }
    }
}
