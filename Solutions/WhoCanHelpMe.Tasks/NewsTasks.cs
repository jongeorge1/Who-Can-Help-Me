namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    using Domain;
    using Domain.Contracts.Services;
    using Domain.Contracts.Tasks;

    #endregion

    public class NewsTasks : INewsTasks
    {
        private readonly INewsService newsService;

        public NewsTasks(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IList<NewsItem> GetProjectBuzz()
        {
            return this.newsService.GetHeadlines();
        }

        public IList<NewsItem> GetDevelopmentTeamBuzz()
        {
            return this.newsService.GetDevTeamHeadlines();
        }
    }
}