namespace WhoCanHelpMe.Web.Registrars
{
    #region Using Directives

    using System.ComponentModel.Composition;
    using Castle.Windsor;

    using Domain.Contracts.Container;


    using SharpArch.Core.CommonValidator;
    using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;

    #endregion

    [Export(typeof(IComponentRegistrar))]
    public class ValidatorRegistrar : IComponentRegistrar
    {
        public void Register(IWindsorContainer container)
        {
            container.AddComponent(
                     "validator",
                     typeof(IValidator),
                     typeof(Validator));
        }
    }
}