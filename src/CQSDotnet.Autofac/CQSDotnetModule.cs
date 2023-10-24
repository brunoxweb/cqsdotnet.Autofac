using Autofac;

namespace CQSDotnet.Autofac
{
    public class CQSDotnetModule : Module
    {
        private readonly System.Reflection.Assembly[] assemblies;

        public CQSDotnetModule(System.Reflection.Assembly[] assemblies)
        {
            this.assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.CQSDotnetModuleRegister(assemblies);
            base.Load(builder);
        }
    }
}