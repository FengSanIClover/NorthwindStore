using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
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
   public class AuthTokensService : Service<AuthTokens>, IAuthTokens
    {
        private IUnitOfWorkAsync unityOfWork;
        private IJWTService jwtService;
        public AuthTokensService(IRepositoryAsync<AuthTokens> repository,[Dependency("Northwind")] IUnitOfWorkAsync unityOfWork, IJWTService jwtService) : base(repository)
        {
            this.unityOfWork = unityOfWork;
            this.jwtService = jwtService;
        }

        public async Task<string> CreateToken(string identity, List<Claim> claims = null, DateTime? expires = null)
        {
            await this.DeleteTokenByAccountAsync(identity);
            var issueAt = DateTime.Now;
            expires = expires ?? issueAt.AddMinutes(10); 
            var jwtToken = await this.jwtService.CreateToken(identity, claims, issueAt, expires);

            //// 寫入 authToken 進資料表
            var authToken = new AuthTokens();
            authToken.AccountID = this.Queryable()
                                      .SingleOrDefault(a => a.Accounts.UserPassword == identity)
                                      .AccountID;
            authToken.Token = jwtToken;
            authToken.IssuedOn = issueAt;
            authToken.ExpiresOn = expires.Value;

            this.Insert(authToken);
            await this.unityOfWork.SaveChangesAsync();

            return await this.jwtService.CreateToken(identity, claims);
        }

        /// <summary>
        /// 刪除對應 Account 的 Token
        /// </summary>
        /// <param name="password"></param>
        /// <returns>成功筆數</returns>
        public async Task<int> DeleteTokenByAccountAsync(string password)
        {
            var authTokens = this.Queryable()
                                 .Where(a => a.AccountID == 1).Select(a => a);

            foreach (var authToken in authTokens)
            {
                try
                {
                    // 原本 authToken 物件的型別為 System.Data.Entity.DynamicProxies.XXXX (循環參考)
                    // 而不能做 CRUD
                    // 解決方法，在 12.Entities 下的 DB.edmx 下的 NorthwindDbContext.cs 檔內建構子加入
                    // this.Configuration.ProxyCreationEnabled = false;
                    // 型別變為 Northwind.Entities.Models.AuthToken 就可做CRUD
                    this.Delete(authToken);
                   // await this.unityOfWork.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return await this.unityOfWork.SaveChangesAsync();
        }

        public async Task<ClaimsPrincipal> ValidateToken(string jwtToken, bool checkExpires)
        {
            var tokenResult = this.Queryable()
                                    .Where(t => t.Token == jwtToken &&
                                                t.ExpiresOn > DateTime.Now)
                                    .Select(t => t)
                                    .SingleOrDefault();

            try
            {
                if (tokenResult != null && tokenResult.ExpiresOn > DateTime.Now)
                {
                    tokenResult.ExpiresOn = DateTime.Now.AddMinutes(10);
                    this.Update(tokenResult);
                    await this.unityOfWork.SaveChangesAsync();
                }
                return await this.jwtService.ValidateToken(jwtToken, checkExpires); 
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
