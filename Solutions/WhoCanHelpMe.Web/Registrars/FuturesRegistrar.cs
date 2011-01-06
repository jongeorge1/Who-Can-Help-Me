namespace WhoCanHelpMe.Web.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;

    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using Framework.Contracts.Container;

    #endregion

    /// <summary>
    /// Hacky way of pulling in the installers from SharpArch.Futures. Once a standard way of registering classes
    /// is established in SharpArch 2, this will be unnecessary.
    /// </summary>
    [Export(typeof(IComponentRegistrar))]
    public class FuturesRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Install(FromAssembly.Named("SharpArch.Futures"));
        }
    }
}