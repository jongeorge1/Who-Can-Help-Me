namespace WhoCanHelpMe.Domain.Contracts.Container
{
    #region Using Directives

    using Castle.Windsor;

    #endregion

    public interface IComponentRegistrar
    {
        void Register(IWindsorContainer container);
    }
}