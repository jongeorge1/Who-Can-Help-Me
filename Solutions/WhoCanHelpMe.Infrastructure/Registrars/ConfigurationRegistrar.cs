namespace WhoCanHelpMe.Infrastructure.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Reflection;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Framework.Contracts.Container;

    using Framework.Extensions;

    using Properties;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class ConfigurationRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(
                    AllTypes.FromAssembly(Assembly.GetAssembly(typeof(InfrastructureRegistrarMarker)))
                            .Pick()
                            .If(f => f.Namespace.Contains("Configuration"))
                            .WithService.DefaultInterface());
        }
    }
}