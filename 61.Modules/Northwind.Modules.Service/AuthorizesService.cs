using Northwind.Domain.Models;
using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Northwind.Modules.Service
{
   public class AuthorizesService : Service<Authorizes>, IAuthorizes
    {
        [Dependency]
        public IAccounts accountService { get; set; }

        [Dependency]
        public IAuthTokens authTokensService { get; set; }

        [Dependency]
        public IJWTService jwtService { get; set; }

        public AuthorizesService(IRepositoryAsync<Authorizes> repository) : base(repository)
        {

        }

        public async Task<UserInfo> CreateUserInfo(string userAccount, string userPassword)
        {
            var result = await this.accountService.Authenticate(userAccount, userPassword);
            if (result != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim("TokenId", userAccount));
                var jwtToken = await this.jwtService.CreateToken(userAccount, claims: claims);
                var account = this.accountService.Queryable()
                                                  .Where(u => u.UserAccount == userAccount)
                                                  .SingleOrDefault();
                var authToken = this.authTokensService.Queryable()
                                                     .Where(a => a.AccountID == account.EmployeeID)
                                                     .SingleOrDefault();
                var userInfo = new UserInfo();

                userInfo.UserAccount = account.UserAccount;
                userInfo.EmployeeName = $"{account.Employees.FirstName} {account.Employees.LastName}";
                userInfo.Token = jwtToken;
                userInfo.TokenExpiredOn = authToken.ExpiresOn;

                return userInfo;
            }
            return null;
        }
    }
}
