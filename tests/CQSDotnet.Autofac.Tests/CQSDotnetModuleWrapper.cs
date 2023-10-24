using Autofac;

namespace CQSDotnet.Autofac.Tests
{
    public class CQSDotnetModuleWrapper : CQSDotnetModule
    {
        public void ExecuteLoad(ContainerBuilder containerBuilder)
        {
            base.Load(containerBuilder);
        }
    }
}
