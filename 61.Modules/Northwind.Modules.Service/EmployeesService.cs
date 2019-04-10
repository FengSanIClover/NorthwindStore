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
   public class EmployeesService : Service<Employees>, IEmployees
    {
        public EmployeesService(IRepositoryAsync<Employees> repository) : base(repository)
        {

        }
    }
}
