namespace WhoCanHelpMe.Infrastructure.Analytics.Configuration
{
    #region Using Directive

    using Domain.Contracts.Configuration;

    #endregion

    public class AnalyticsConfiguration : IAnalyticsConfiguration
    {
        public string Idenfitier
        {
            get { return AnalyticsConfigurationSection.Instance.AnalyticsIdentifier; }
        }

        public string Verification
        {
            get { return AnalyticsConfigurationSection.Instance.SiteVerification; }
        }
    }
}