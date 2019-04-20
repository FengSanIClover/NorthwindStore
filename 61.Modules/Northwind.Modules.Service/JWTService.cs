using Microsoft.IdentityModel.Tokens;
using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using Northwind.Modules.Service.Extensions;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Northwind.Modules.Service
{
    public class JWTService : IJWTService
    {
        [Dependency]
        public IAuthTokens authService { get; set; }

        [Dependency("Northwind")]
        public IUnitOfWorkAsync unityOfWork { get; set; }

        [Dependency]
        public IEmployees employeeService { get; set; }

        private static string signSecKey;

        public JWTService()
        {
            JWTService.signSecKey = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb33759";
        }

        public async Task<string> CreateToken(string identity, List<Claim> claims = null, DateTime? expires = null)
        {
            await this.DeleteTokenByAccountAsync(identity);

            // Set issued at date 必須是 DateTime.UtcNow
            DateTime issuedAt = DateTime.UtcNow;

            // Create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.Name,identity)
            });

            //Claim claim = new Claim(ClaimTypes.Name, identity);
            //claimsIdentity.AddClaim(claim);

            if (claims != null)
            {
                claimsIdentity.AddClaims(claims);
            }

            var signingCredentials = JWTService.signSecKey.CreateSymmetricSecurityKey().CreateSigningCredentials();

            var encryptingCredentials = JWTService.signSecKey.CreateSymmetricSecurityKey().CreateEncryptingCredentials();

            var tokenHandler = new JwtSecurityTokenHandler();

            // Create the jwtToken
            var jwtSecurityToken =
                    tokenHandler.CreateJwtSecurityToken(
                        issuer: "Northwind",
                        audience: "Northwind",
                        subject: claimsIdentity,
                        notBefore: issuedAt,
                        issuedAt: issuedAt,
                        expires: issuedAt.AddMinutes(5),// expires,
                        signingCredentials: signingCredentials ,encryptingCredentials: encryptingCredentials
                        );
            // 
            var jwtToken = tokenHandler.WriteToken(jwtSecurityToken);

            var authToken = new AuthTokens();

            authToken.AccountID = this.employeeService.GetOne(identity).Result.EmployeeID;
            authToken.Token = jwtToken;
            authToken.IssuedOn = issuedAt;
            authToken.ExpiresOn = issuedAt.AddMinutes(5);

            this.authService.Insert(authToken);

            // 發生 System.Data.Entity.Validation.DbEntityValidationException 
            //原因: 資料庫 Token 欄位長度不足 jwtToken 的長度
            await this.unityOfWork.SaveChangesAsync();

            return jwtToken;
        }

        /// <summary>
        /// 判斷此 Token 是否存在，並有效，有效會再重設時間 10 分鐘
        /// </summary>
        /// <param name="jwtToken"></param>
        /// <param name="checkExpires"></param>
        /// <returns></returns>
        public async Task<ClaimsPrincipal> ValidateToken(string jwtToken, bool checkExpires)
        {
            var securityKey = JWTService.signSecKey.CreateSymmetricSecurityKey();

            SecurityToken securityToken;

            var tokenHandler = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidAudience = "Northwind";
            validationParameters.ValidIssuer = "Northwind";
            validationParameters.ValidateLifetime = checkExpires;
            validationParameters.LifetimeValidator = this.LifetimeValidator;
            validationParameters.ValidateIssuerSigningKey = true;
            validationParameters.IssuerSigningKey = securityKey;

            // Validate jwtToken
            var tokenResult = this.authService.Queryable()
                         .Where(t => t.Token == jwtToken &&
                                     t.ExpiresOn > DateTime.Now)
                         .Select(t => t)
                         .SingleOrDefault();
            try
            {
                if (tokenResult != null && tokenResult.ExpiresOn > DateTime.Now)
                {
                    tokenResult.ExpiresOn = DateTime.Now.AddMinutes(10);
                    this.authService.Update(tokenResult);
                    await this.unityOfWork.SaveChangesAsync();
                }
                return tokenHandler.ValidateToken(jwtToken, validationParameters, out securityToken);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires,
            SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

        /// <summary>
        /// 刪除對應 Account 的 Token
        /// </summary>
        /// <param name="password"></param>
        /// <returns>成功筆數</returns>
        public async Task<int> DeleteTokenByAccountAsync(string password)
        {
            var authTokens = this.authService.Queryable()
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
                    this.authService.Delete(authToken);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return await this.unityOfWork.SaveChangesAsync();
        }
    }
}
