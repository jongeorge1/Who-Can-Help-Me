namespace WhoCanHelpMe.Infrastructure.Registrars
{
    using System;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using SharpArch.Data.NHibernate;

    public class SessionKeyProviderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISessionFactoryKeyProvider>().ImplementedBy<DefaultSessionFactoryKeyProvider>());
        }
    }
}