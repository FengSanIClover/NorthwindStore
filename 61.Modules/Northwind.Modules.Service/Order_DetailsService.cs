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
   public class Order_DetailsService : Service<Order_Details>, IOrder_Details
    {
        public Order_DetailsService(IRepositoryAsync<Order_Details> repository) : base(repository)
        {

        }
    }
}
