namespace WhoCanHelpMe.Web.Initialisers
{
    #region Using Directives

    using System;
    using System.ComponentModel.Composition;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Code;

    using NHibernate.Validator.Event;

    #endregion

    public class ValidationInitialiser : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var provider = new NHibernateSharedEngineProvider();
            NHibernate.Validator.Cfg.Environment.SharedEngineProvider = provider;

            // Configure the xVal validation framework to accept validation rules from NHibernate.Validator
            xVal.ActiveRuleProviders.Providers.Add(new ValidatorRulesProvider());
        }
    }
}