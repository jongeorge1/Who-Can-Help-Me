namespace WhoCanHelpMe.Web.Initialisers
{
    #region Using Directives

    using System.ComponentModel.Composition;

    using Framework.Contracts.Container;

    using Code;

    using NHibernate.Validator.Event;

    #endregion

    [Export(typeof(IComponentInitialiser))]
    public class ValidationInitialiser : IComponentInitialiser
    {
        public void Initialise()
        {
            var provider = new NHibernateSharedEngineProvider();
            NHibernate.Validator.Cfg.Environment.SharedEngineProvider = provider;

            // Configure the xVal validation framework to accept validation rules from NHibernate.Validator
            xVal.ActiveRuleProviders.Providers.Add(new ValidatorRulesProvider());
        }
    }
}