using System.Reflection;
using Autofac;

namespace CQSDotnet.Autofac.Tests
{
    public class CQSDotnetModuleWrapper : CQSDotnetModule
    {
        public CQSDotnetModuleWrapper(Assembly[] assemblies) : base(assemblies)
        {
        }

        public void ExecuteLoad(ContainerBuilder containerBuilder)
        {
            base.Load(containerBuilder);
        }
    }
}
