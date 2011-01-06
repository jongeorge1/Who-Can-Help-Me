namespace WhoCanHelpMe.Web.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;

    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Framework.Contracts.Container;


    using SharpArch.Core.CommonValidator;
    using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class ValidatorRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.Register(Component.For(typeof(IValidator)).ImplementedBy(typeof(Validator)).Named("validator"));
        }
    }
}