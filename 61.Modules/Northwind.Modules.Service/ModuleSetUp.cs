using Northwind.Modules.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinkpower.ModuleResolver;

namespace Northwind.Modules.Service
{
    [Export(typeof(IModule))]
    public class ModuleSetUp : IModule
    {
        public void SetUp(IModuleRegister registrar)
        {
            registrar.RegisterType<IAccounts, AccountsService>();
            registrar.RegisterType<IAuthorizes, AuthorizesService>();
            registrar.RegisterType<IAuthTokens, AuthTokensService>();
            registrar.RegisterType<ICategories, CategoriesService>();
            registrar.RegisterType<ICustomers, CustomersService>();
            registrar.RegisterType<IEmployees, EmployeesService>();
            registrar.RegisterType<IOrder_Details, Order_DetailsService>();
            registrar.RegisterType<IOrders, OrdersService>();
            registrar.RegisterType<IProducts, ProductsService>();
            registrar.RegisterType<IRegion, RegionService>();
            registrar.RegisterType<IShippers, ShippersService>();
            registrar.RegisterType<ISuppliers, SuppliersService>();
            registrar.RegisterType<ITerritories, TerritoriesService>();
        }
    }
}
