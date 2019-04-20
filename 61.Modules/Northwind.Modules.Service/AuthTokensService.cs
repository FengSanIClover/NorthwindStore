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
        public AuthTokensService(IRepositoryAsync<AuthTokens> repository) : base(repository)
        {
            
        }  
    }
}
