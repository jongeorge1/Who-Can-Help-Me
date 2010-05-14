namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface INewsTasks
    {
        IList<NewsItem> GetProjectBuzz();
        IList<NewsItem> GetDevelopmentTeamBuzz();
    }
}