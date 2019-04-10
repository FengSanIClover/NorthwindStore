using Northwind.Entities.Models;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinkpower.ModuleResolver;

namespace Northwind.Entities
{
    [Export(typeof(IModule))]
    public class ModuleSetUp : IModule
    {
        public void SetUp(IModuleRegister registrar)
        {
            registrar.RegisterDataContext<DbContext, NorthwindDbContext>();
            registrar.RegisterUnitOfWork<IUnitOfWorkAsync, UnitOfWork>();
            registrar.RegisterRepository<IRepositoryAsync<Accounts>, Repository<Accounts>>();
            registrar.RegisterRepository<IRepositoryAsync<Authorizes>, Repository<Authorizes>>();
            registrar.RegisterRepository<IRepositoryAsync<AuthTokens>, Repository<AuthTokens>>();
            registrar.RegisterRepository<IRepositoryAsync<Categories>, Repository<Categories>>();
            registrar.RegisterRepository<IRepositoryAsync<Customers>, Repository<Customers>>();
            registrar.RegisterRepository<IRepositoryAsync<Employees>, Repository<Employees>>();
            registrar.RegisterRepository<IRepositoryAsync<Order_Details>, Repository<Order_Details>>();
            registrar.RegisterRepository<IRepositoryAsync<Orders>, Repository<Orders>>();
            registrar.RegisterRepository<IRepositoryAsync<Products>, Repository<Products>>();
            registrar.RegisterRepository<IRepositoryAsync<Region>, Repository<Region>>();
            registrar.RegisterRepository<IRepositoryAsync<Shippers>, Repository<Shippers>>();
            registrar.RegisterRepository<IRepositoryAsync<Suppliers>, Repository<Suppliers>>();
            registrar.RegisterRepository<IRepositoryAsync<Territories>, Repository<Territories>>();
        }
    }
}
