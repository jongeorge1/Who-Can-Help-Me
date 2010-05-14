namespace WhoCanHelpMe.Infrastructure.News.Configuration
{
    #region Using Directives

    using System.Linq;
    using System.Collections.Generic;
    using Domain.Contracts.Configuration;

    #endregion

    public class NewsConfiguration : INewsConfiguration
    {
        private readonly NewsConfigurationSection configSection;
        
        public NewsConfiguration()
        {
            configSection = NewsConfigurationSection.Instance;
        }

        public IList<string> BuzzHeadlineTags
        {
            get
            {
                return (from SearchTag tag in configSection.BuzzHeadlineTags select tag.Name).ToList();
            }
        }

        public IList<string> DevTeamHeadlineTags
        {
            get
            {
                return (from SearchTag tag in configSection.DevTeamHeadlineTags select tag.Name).ToList();
            }
        }

        public int NoOfBuzzHeadlines
        {
            get { return configSection.NoOfBuzzHeadlines; }
        }

        public int NoOfDevTeamHeadlines
        {
            get { return configSection.NoOfDevTeamHeadlines; }
        }

        public int SearchTimeoutSeconds
        {
            get { return configSection.SearchTimeoutSeconds; }
        }
    }
}