using Autofac;

namespace CQSDotnet.Autofac.Tests
{
    [TestFixture]
    public class CQSDotnetModuleTests
    {
        [Test]
        public void Load_RegistersAssemblies()
        {
            // Arrange
            var builder = new ContainerBuilder();
            var module = new CQSDotnetModuleWrapper();

            // Act
            module.ExecuteLoad(builder);
            var container = builder.Build();

            var registeredAssemblies = container.ComponentRegistry.Registrations
                .Select(registration => registration.Activator.LimitType.Assembly)
                .Distinct();

            // Assert
            Assert.That(registeredAssemblies, Is.Not.Null);
            Assert.That(registeredAssemblies.Count(), Is.EqualTo(2));
        }
    }
}