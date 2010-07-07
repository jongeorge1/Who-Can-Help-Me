namespace WhoCanHelpMe.Web.Controllers.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using System.Web.Mvc;

    using Castle.Windsor;

    using Framework.Contracts.Container;

    using MvcContrib.Castle;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class ControllerFactoryRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}