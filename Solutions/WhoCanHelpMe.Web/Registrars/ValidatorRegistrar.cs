namespace WhoCanHelpMe.Web.Registrars
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using SharpArch.Core.CommonValidator;
    using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;

    public class ValidatorRegistrar : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IValidator)).ImplementedBy(typeof(Validator)).Named("validator"));
        }
    }
}