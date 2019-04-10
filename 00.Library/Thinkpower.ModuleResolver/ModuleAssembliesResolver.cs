using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
//using System.Web.Http.Dispatcher;

namespace Thinkpower.ModuleResolver
{
    //public class ModuleAssembliesResolver : DefaultAssembliesResolver
    //{
    //    private readonly string modulePath;

    //    public ModuleAssembliesResolver(string modulePath)
    //    {
    //        this.modulePath = modulePath;
    //    }

    //    public override ICollection<Assembly> GetAssemblies()
    //    {
    //        ICollection<Assembly> baseAssemblies = base.GetAssemblies();
    //        List<Assembly> assemblies = new List<Assembly>(baseAssemblies);

    //        if (Directory.Exists(modulePath))
    //        {
    //            Directory.GetFiles(modulePath).ToList().ForEach(o =>
    //            {
    //                var controllersAssembly = Assembly.LoadFrom(o);
    //                baseAssemblies.Add(controllersAssembly);
    //            });
    //        }

    //        return assemblies;
    //    }
    //}
}
