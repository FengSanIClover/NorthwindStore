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
        private IAccounts accountsService;
        private IJWTService jwtService; 
        private IAuthTokens authTokenService;
        public AuthController(IAccounts accountsService, IAuthTokens authTokenService,IJWTService jwtService)
        {
            this.accountsService = accountsService;
            this.jwtService = jwtService;
            this.authTokenService = authTokenService;
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
            var result = await this.accountsService.Authenticate(loginReq.UserAccount, loginReq.UserPassword);
            if (result != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("TokenId", loginReq.UserAccount));
                var jwtToken = await this.jwtService.CreateToken(loginReq.UserAccount, claims: claims);
                var account = this.accountsService.Queryable()
                                                  .Where(u => u.UserAccount == loginReq.UserAccount)
                                                  .SingleOrDefault();
                var authToken = this.authTokenService.Queryable()
                                                     .Where(a => a.AccountID == account.EmployeeID)
                                                     .SingleOrDefault();
                var userInfo = new UserInfo();
                userInfo.UserAccount = account.UserAccount;
                userInfo.EmployeeName = $"{account.Employees.FirstName} {account.Employees.LastName}";
                userInfo.Token = jwtToken;
                userInfo.TokenExpiredOn = authToken.ExpiresOn;

                return Success(userInfo);
            }
            return Unauthorized();
        }

        [Route("GetAll")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAll()
        {
            return Success(this.authTokenService.Queryable());
        }
    }
}
