namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    
    using Domain;
    using Domain.Contracts.Tasks;
    using Domain.Contracts.Configuration;

    #endregion

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
                                       Scripts = this.GetScripts(),
                                       SiteVerification = this.configurationService.Analytics.Verification, 
                                       Styles = this.GetStyles(),
                                       Title = "Who Can Help Me?"
                                   };

            return siteMetaData;
        }

        private IList<string> GetScripts()
        {
            var scripts = new List<string>
                              {
                                  "jquery-1.4.1.min.js",
                                  "jquery.autocomplete.custom.js",
                                  "jquery.validate.js",
                                  "xVal.jquery.validate.js",
                                  "openid-jquery.js",
                                  "bespoke.js"
                              };

            return scripts;
        }

        private IList<string> GetStyles()
        {
            var styles = new List<string>
                              {
                                  "reset.css",
                                  "jquery.autocomplete.css",
                                  "openid.css",
                                  "site.less"
                              };

            return styles;
        }
    }
}