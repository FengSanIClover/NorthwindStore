using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thinkpower.ModuleResolver
{
    /// <summary>
    /// To register all the internal type with unity.
    /// </summary>
    public interface IModule
    {
        void SetUp(IModuleRegister registrar);
    }
}
