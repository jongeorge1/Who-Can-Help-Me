namespace WhoCanHelpMe.Tasks
{
    using Domain;
    using Domain.Contracts.Tasks;
    using Domain.Contracts.Configuration;

    public class SiteMetaDataTasks : ISiteMetaDataTasks
    {
        private readonly IConfigurationService configurationService;

        public SiteMetaDataTasks(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public SiteMetaData GetSiteMetaData()
        {
            var siteMetaData = new SiteMetaData
                                   {
                                       AnalyticsIdentifier = this.configurationService.Analytics.Idenfitier,
                                       SiteVerification = this.configurationService.Analytics.Verification, 
                                   };

            return siteMetaData;
        }
    }
}