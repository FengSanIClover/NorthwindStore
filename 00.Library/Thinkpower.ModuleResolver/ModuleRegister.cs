using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

using Repository.Pattern.UnitOfWork;
using System.Data.Entity;

namespace Thinkpower.ModuleResolver
{
    internal class ModuleRegister : IModuleRegister
    {
        private readonly IUnityContainer _container;

        public ModuleRegister(IUnityContainer container)
        {
            this._container = container;
            //Register interception behaviour if any
        }

        public void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            if (withInterception)
            {
                //register with interception 
            }
            else
            {
                this._container.RegisterType<TFrom, TTo>();
            }
        }

        public void RegisterType<TFrom, TTo>(string name, bool withInterception = false) where TTo : TFrom
        {
            if (withInterception)
            {
                //register with interception 
            }
            else
            {
                this._container.RegisterType<TFrom, TTo>(name);
            }
        }

        public void RegisterTypeWithPerRequestLife<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(new PerRequestLifetimeManager());
        }

        public void RegisterTypeWithContainerControlledLife<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        #region URF Framework Register

        public void RegisterDataContext<TFrom, TTo>(string name = "") where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(name, new PerRequestLifetimeManager());
        }

        public void RegisterUnitOfWork<TFrom, TTo>(string name = "") where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(name, new PerRequestLifetimeManager(),
                new InjectionConstructor(
                    new ResolvedParameter<DbContext>(name)));
        }

        public void RegisterRepository<TFrom, TTo>(string name = "") where TTo : TFrom
        {
            this._container.RegisterType<TFrom, TTo>(
              new InjectionConstructor(
                  new ResolvedParameter<DbContext>(name),
                  new ResolvedParameter<IUnitOfWorkAsync>(name)));
        }

        #endregion
    }
}
