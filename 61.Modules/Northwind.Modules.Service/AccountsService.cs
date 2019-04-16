using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Modules.Service
{
   public class AccountsService : Service<Accounts>,IAccounts
    {
        public AccountsService(IRepositoryAsync<Accounts> repository) : base(repository)
        {

        }

        public async Task<Employees> Authenticate(string userAccount, string userPassword)
        {
            var result = this.Queryable()
                             .Where(u => u.UserAccount == userAccount && u.UserPassword == userPassword)
                             .Select(u => u.Employees)
                             .SingleOrDefault();
            return  result;

        }
    }
}
