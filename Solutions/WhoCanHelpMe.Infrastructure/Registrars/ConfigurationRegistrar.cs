namespace WhoCanHelpMe.Infrastructure.Registrars
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    #endregion

    public class ConfigurationRegistrar : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    AllTypes.FromAssembly(Assembly.GetAssembly(typeof(ConfigurationRegistrar)))
                            .Pick()
                            .If(f => f.Namespace.Contains("Configuration"))
                            .WithService.DefaultInterface());
        }
    }
}