using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Northwind.Modules.Service
{
    public class EmployeesService : Service<Employees>, IEmployees
    {
        [Dependency]
        public IAccounts accountService { get; set; }

        public EmployeesService(IRepositoryAsync<Employees> repository) : base(repository)
        {

        }

        public async Task<Employees> GetOne(string userAccount)
        {
            return  this.accountService.Queryable().SingleOrDefault(a => a.UserAccount == userAccount).Employees;
        }
    }
}
