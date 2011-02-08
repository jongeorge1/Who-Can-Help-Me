namespace WhoCanHelpMe.Infrastructure.Registrars
{
    #region Using Directives

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using WhoCanHelpMe.Domain.Contracts.Services;
    using WhoCanHelpMe.Framework.Security;
    using WhoCanHelpMe.Infrastructure.News;
    using WhoCanHelpMe.Infrastructure.Security;

    #endregion

    public class ServiceRegistrar : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<INewsService>().ImplementedBy<TwitterNewsService>());
            container.Register(Component.For<IIdentityService>().ImplementedBy<IdentityService>());
        }
    }
}