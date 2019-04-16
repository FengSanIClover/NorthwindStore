using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Modules.Service.Extensions
{
   public static class JWTExtension
    {
        /// <summary>
        /// 產生對稱安全金鑰
        /// </summary>
        /// <param name="value">自定義的安全碼</param>
        /// <returns>金鑰</returns>
        public static SymmetricSecurityKey CreateSymmetricSecurityKey(this string value)
        {
            return new SymmetricSecurityKey(Encoding.Default.GetBytes(value));
        }

        /// <summary>
        /// 產生安全憑證
        /// </summary>
        /// <param name="securityKey">對稱安全金鑰</param>
        /// <returns>憑證</returns>
        public static SigningCredentials CreateSigningCredentials(this SymmetricSecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        /// <summary>
        /// 產生加密的安全憑證
        /// </summary>
        /// <param name="securityKey">對稱安全金鑰</param>
        /// <returns>加密的安全憑證</returns>
        public static EncryptingCredentials CreateEncryptingCredentials(this SymmetricSecurityKey securityKey)
        {
            return new EncryptingCredentials(securityKey, JwtConstants.DirectKeyUseAlg, SecurityAlgorithms.Aes256CbcHmacSha512);
        }
    }
}
