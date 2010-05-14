namespace MSpecTests.WhoCanHelpMe
{
    #region Using Directives

    using global::WhoCanHelpMe.Framework.Caching;

    using Microsoft.Practices.ServiceLocation;

    using Rhino.Mocks;

    using SharpArch.Core.CommonValidator;
    using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;

    #endregion

    public static class ServiceLocatorHelper
    {
        private static IServiceLocator provider;

        public static void InitialiseServiceLocator()
        {
            provider = MockRepository.GenerateStub<IServiceLocator>();

            ServiceLocator.SetLocatorProvider(() => provider);
        }

        public static IValidator AddValidator()
        {
            if (provider == null)
            {
                InitialiseServiceLocator();
            }

            var validator = new Validator();
            provider.Stub(p => p.GetInstance<IValidator>()).Return(validator);

            return validator;
        }

        public static ICachingService AddCachingService()
        {
            if (provider == null)
            {
                InitialiseServiceLocator();
            }

            var cachingService = MockRepository.GenerateStub<ICachingService>();
            cachingService.Stub(c => c[null]).IgnoreArguments().Return(null);
            cachingService.AddToServiceLocator();
            return cachingService;
        }

        public static T AddToServiceLocator<T>(this T o)
        {
            if (provider == null)
            {
                InitialiseServiceLocator();
            }

            provider.Stub(p => p.GetInstance<T>()).Return(o);
            return o;
        }
    }
}
