using Autofac;

namespace CQSDotnet.Autofac
{
    public class AutofacTypeResolver : ITypeResolver
    {
        private readonly IComponentContext componentContext;

        public AutofacTypeResolver(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public object Resolve(Type type)
        {
            return this.componentContext.Resolve(type);
        }
    }
}
