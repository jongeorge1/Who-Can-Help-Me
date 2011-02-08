namespace WhoCanHelpMe.Web.Controllers.Registrars
{
    #region Using Directives

    using System.Web.Mvc;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using SharpArch.Web.Castle;

    #endregion

    public class ControllerFactoryRegistrar : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}