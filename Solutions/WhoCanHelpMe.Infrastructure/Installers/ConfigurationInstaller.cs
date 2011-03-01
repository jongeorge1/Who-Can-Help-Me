namespace WhoCanHelpMe.Infrastructure.Installers
{
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class ConfigurationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    AllTypes.FromAssembly(Assembly.GetAssembly(typeof(ConfigurationInstaller)))
                            .Pick()
                            .If(f => f.Namespace.Contains("Configuration"))
                            .WithService.DefaultInterface());
        }
    }
}