using System;
using System.Collections.Generic;

namespace Thinkpower.ModuleResolver
{
    /// <summary>
    /// Responsible for registering types in unity configuration by implementing IModule
    /// </summary>
    public interface IModuleRegister
    {
        void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;

        void RegisterType<TFrom, TTo>(string name, bool withInterception = false) where TTo : TFrom;

        void RegisterTypeWithPerRequestLife<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;

        void RegisterTypeWithContainerControlledLife<TFrom, TTo>(bool withInterception = false) where TTo : TFrom;

        #region URF Framework Register

        void RegisterDataContext<TFrom, TTo>(string name = "") where TTo : TFrom;

        void RegisterUnitOfWork<TFrom, TTo>(string name = "") where TTo : TFrom;

        void RegisterRepository<TFrom, TTo>(string name = "") where TTo : TFrom;

        #endregion
    }
}
