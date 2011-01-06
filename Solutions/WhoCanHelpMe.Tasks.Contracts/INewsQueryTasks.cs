namespace WhoCanHelpMe.Tasks.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    using WhoCanHelpMe.Domain;

    #endregion

    public interface INewsQueryTasks
    {
        IList<NewsItem> GetProjectBuzz();

        IList<NewsItem> GetDevelopmentTeamBuzz();
    }
}