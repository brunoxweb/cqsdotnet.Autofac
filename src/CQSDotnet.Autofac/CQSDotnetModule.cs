using Autofac;

namespace CQSDotnet.Autofac
{
    public class CQSDotnetModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.CQSDotnetModuleRegister(assemblies.ToArray());
            base.Load(builder);
        }
    }
}