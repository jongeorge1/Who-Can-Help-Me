namespace WhoCanHelpMe.Infrastructure.Configuration
{
    #region Using Directives

    using Domain.Contracts.Configuration;
    using Domain.Contracts.Services;

    #endregion

    public class ConfigurationService : IConfigurationService
    {
        private readonly IAnalyticsConfiguration analytics;
        private readonly INewsConfiguration news;

        public ConfigurationService(IAnalyticsConfiguration analytics, INewsConfiguration news)
        {
            this.analytics = analytics;
            this.news = news;
        }

        public IAnalyticsConfiguration Analytics
        {
            get { return analytics; }
        }

        public INewsConfiguration News
        {
            get { return news; }
        }
    }
}