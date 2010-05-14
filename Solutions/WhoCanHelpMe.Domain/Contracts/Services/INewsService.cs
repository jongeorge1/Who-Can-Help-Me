namespace WhoCanHelpMe.Domain.Contracts.Services
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface INewsService
    {
        IList<NewsItem> GetHeadlines();
        IList<NewsItem> GetDevTeamHeadlines();
    }
}