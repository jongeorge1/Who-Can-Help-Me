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

    using WhoCanHelpMe.Domain.Contracts.Services;
    using WhoCanHelpMe.Framework.Security;
    using WhoCanHelpMe.Infrastructure.News;
    using WhoCanHelpMe.Infrastructure.Security;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class ServiceRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(Component.For<INewsService>().ImplementedBy<TwitterNewsService>());
            container.Register(Component.For<IIdentityService>().ImplementedBy<IdentityService>());            
        }
    }
}