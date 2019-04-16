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
        private static string signSecKey;

        public JWTService()
        {
            JWTService.signSecKey = "MySecurityKey";
        }

        public async Task<string> CreateToken(string identity, List<Claim> claims = null,DateTime? issueAt = null, DateTime? expires = null)
        {

            // Create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
               new Claim(ClaimTypes.Name,identity)
            });

            // Claim claim = new Claim(ClaimTypes.Name,identity);
            // claimsIdentity.AddClaim(claim);

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
                        notBefore: issueAt,
                        issuedAt: issueAt,
                        expires: expires,
                        signingCredentials: signingCredentials,
                        encryptingCredentials: encryptingCredentials);

            return tokenHandler.WriteToken(jwtSecurityToken);
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
            try
            {
                //if (tokenResult != null && tokenResult.ExpiresOn > DateTime.Now)
                //{
                //    tokenResult.ExpiresOn = DateTime.Now.AddMinutes(10);
                //    this.authTokensService.Update(tokenResult);
                //    await this.unityOfWork.SaveChangesAsync();
                //}
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
    }
}
