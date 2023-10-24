using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Autofac;
using CQSDotnet.Commands;
using CQSDotnet.Commands.Interfaces;
using CQSDotnet.Queries;
using CQSDotnet.Queries.Interfaces;

namespace CQSDotnet.Autofac
{
    [ExcludeFromCodeCoverage]
    internal static class CQSDotnetModuleExtensions
    {
        internal static ContainerBuilder CQSDotnetModuleRegister(this ContainerBuilder containerBuilder, Assembly[] assemblies)
        {
            containerBuilder.RegisterType<AutofacTypeResolver>().As<ITypeResolver>().SingleInstance();

            foreach (var assembly in assemblies)
            {
                containerBuilder
                   .ScanAssemblies(assembly, typeof(IQuery), typeof(IQueryHandler<,>))
                   .ScanAssemblies(assembly, typeof(IQuery), typeof(IQueryValidator<>))
                   .ScanAssemblies(assembly, typeof(ICommand), typeof(ICommandHandler<>))
                   .ScanAssemblies(assembly, typeof(ICommand), typeof(ICommandValidator<>));
            }

            containerBuilder.RegisterType<QueryHandlerFactory>().As<IQueryHandlerFactory>();
            containerBuilder.RegisterType<QueryValidatorFactory>().As<IQueryValidatorFactory>();

            containerBuilder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>();
            containerBuilder.RegisterType<CommandValidatorFactory>().As<ICommandValidatorFactory>();

            containerBuilder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            containerBuilder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();

            return containerBuilder;
        }

        private static ContainerBuilder ScanAssemblies(this ContainerBuilder containerBuilder, Assembly assembly, Type serviceType, Type handlerType)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(type => serviceType.IsAssignableFrom(type))
                .ToList();

            for (int i = 0; i < serviceTypes.Count; i++)
            {
                var typeToRegister = serviceTypes[i].Assembly.GetTypes()
                  .FirstOrDefault(vt => vt.GetInterfaces().Any(a => a.IsGenericType &&
                             a.GetGenericTypeDefinition() == handlerType &&
                             a.GetGenericArguments()[0] == serviceTypes[i]));

                if (typeToRegister != null)
                {
                    containerBuilder.RegisterType(typeToRegister).AsImplementedInterfaces();
                }
            }

            return containerBuilder;
        }
    }
}