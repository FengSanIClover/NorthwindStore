using Northwind.Entities.Models;
using Northwind.Modules.Interface;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Northwind.Modules.Service
{
   public class ProductsService : Service<Products>, IProducts
    {
        [Dependency("Northwind")]
        public IUnitOfWorkAsync unityOfWork { get; set; }

        public ProductsService(IRepositoryAsync<Products> repository) : base(repository)
        {
          //  this.unityOfWork = unityOfWork;, IUnitOfWorkAsync unityOfWork
        }
    }
}
