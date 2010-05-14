namespace WhoCanHelpMe.Domain.Contracts.Configuration
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface INewsConfiguration
    {
        IList<string> BuzzHeadlineTags { get; }
        IList<string> DevTeamHeadlineTags { get; }
        int NoOfBuzzHeadlines { get; }
        int NoOfDevTeamHeadlines { get; }
        int SearchTimeoutSeconds { get; }
    }
}