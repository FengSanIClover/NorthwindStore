﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Northwind.Entities
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.ComponentModel.Composition;
     // 修改這裡2 
    using Thinkpower.ModuleResolver;
    using Thinkpower.ModuleResolver.URF;
    
    using Repository.Pattern.UnitOfWork;
    using Repository.Pattern.Repositories;
    using Repository.Pattern.Ef6;
    
    using Northwind.Entities.Models;
    
    [Export(typeof(IModule))]
    public class ModuleSetUp : IModule
    {
    	public void SetUp(IModuleRegister register)
    	{
    		// TODO Config: 因為有多個資料庫，所以在註冊時指定名稱，讓 Service 可以透過名稱取得正確的 IDataContextAsync 及 IUnitOfWorkAsync。
    		// 修改這裡3
    		register.RegisterDataContext<DbContext, NorthwindDbContext>("Northwind");
    		register.RegisterUnitOfWork<IUnitOfWorkAsync, UnitOfWork>("Northwind");
    		// register.RegisterDataContext<IDataContextHelper, NorthwindDbContext>("Northwind");
    
            register.RegisterRepository<IRepositoryAsync<Accounts>, Repository<Accounts>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Authorizes>, Repository<Authorizes>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<AuthTokens>, Repository<AuthTokens>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Categories>, Repository<Categories>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Customers>, Repository<Customers>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Employees>, Repository<Employees>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Order_Details>, Repository<Order_Details>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Orders>, Repository<Orders>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Products>, Repository<Products>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Region>, Repository<Region>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Shippers>, Repository<Shippers>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Suppliers>, Repository<Suppliers>>("Northwind");
            register.RegisterRepository<IRepositoryAsync<Territories>, Repository<Territories>>("Northwind");
        }
    }
}
