using System.Reflection;
using Autofac;
using CQSDotnet.Autofac.Tests.Stubs;
using CQSDotnet.Commands;
using CQSDotnet.Commands.Interfaces;
using CQSDotnet.Queries;
using CQSDotnet.Queries.Interfaces;

namespace CQSDotnet.Autofac.Tests
{
    [TestFixture]
    public class CQSDotnetModuleTests
    {
        [Test]
        public void Load_RegistersTypes_RegisteredSuccessfully()
        {
            // Arrange
            var builder = new ContainerBuilder();
            var assemblies = new Assembly[] { typeof(CQSDotnetModuleTests).Assembly };
            var module = new CQSDotnetModuleWrapper(assemblies);

            // Act
            module.ExecuteLoad(builder);
            var container = builder.Build();

            // Assert
            AssertIsRegistered<ITypeResolver, AutofacTypeResolver>(container);
            AssertIsRegistered<IQueryHandlerFactory, QueryHandlerFactory>(container);
            AssertIsRegistered<IQueryValidatorFactory, QueryValidatorFactory>(container);
            AssertIsRegistered<ICommandHandlerFactory, CommandHandlerFactory>(container);
            AssertIsRegistered<ICommandValidatorFactory, CommandValidatorFactory>(container);
            AssertIsRegistered<ICommandDispatcher, CommandDispatcher>(container);
            AssertIsRegistered<IQueryDispatcher, QueryDispatcher>(container);
        }

        [Test]
        public void Load_ScansAssemblies_RegisteredSuccessfully()
        {
            // Arrange
            var builder = new ContainerBuilder();
            var assemblies = new Assembly[] { typeof(CQSDotnetModuleTests).Assembly };
            var module = new CQSDotnetModuleWrapper(assemblies);

            // Act
            module.ExecuteLoad(builder);
            var container = builder.Build();

            // Assert
            AssertIsRegistered<IQueryHandler<MyQuery, MyDto>, MyQueryHandler>(container);
        }

        private static bool AssertIsRegistered<TInterface, TImplementation>(IContainer container)
        {
            return container.IsRegisteredWithName(nameof(TInterface), typeof(TImplementation));
        }
    }
}