namespace WhoCanHelpMe.Infrastructure.News.Configuration
{
    using System.Collections.Generic;
    using System.Linq;

    using WhoCanHelpMe.Infrastructure.News.Contracts;

    public class NewsConfiguration : INewsConfiguration
    {
        private readonly NewsConfigurationSection configSection;

        public NewsConfiguration()
        {
            this.configSection = NewsConfigurationSection.Instance;
        }

        public IList<string> BuzzHeadlineTags
        {
            get
            {
                return (from SearchTag tag in this.configSection.BuzzHeadlineTags select tag.Name).ToList();
            }
        }

        public IList<string> DevTeamHeadlineTags
        {
            get
            {
                return (from SearchTag tag in this.configSection.DevTeamHeadlineTags select tag.Name).ToList();
            }
        }

        public int NoOfBuzzHeadlines
        {
            get
            {
                return this.configSection.NoOfBuzzHeadlines;
            }
        }

        public int NoOfDevTeamHeadlines
        {
            get
            {
                return this.configSection.NoOfDevTeamHeadlines;
            }
        }

        public int SearchTimeoutSeconds
        {
            get
            {
                return this.configSection.SearchTimeoutSeconds;
            }
        }
    }
}