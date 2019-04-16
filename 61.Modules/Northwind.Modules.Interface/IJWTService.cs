using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Modules.Interface
{
   public interface IJWTService
    {
        Task<string> CreateToken(string identity, List<Claim> claims = null, DateTime? issueAt = null, DateTime? expires = null);

        Task <ClaimsPrincipal> ValidateToken(string jwtToken, bool checkExpires);
    }
}
