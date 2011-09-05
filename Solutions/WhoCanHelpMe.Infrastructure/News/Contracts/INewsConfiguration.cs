namespace WhoCanHelpMe.Infrastructure.News.Contracts
{
    using System.Collections.Generic;

    public interface INewsConfiguration
    {
        IList<string> BuzzHeadlineTags { get; }

        IList<string> DevTeamHeadlineTags { get; }

        int NoOfBuzzHeadlines { get; }

        int NoOfDevTeamHeadlines { get; }

        int SearchTimeoutSeconds { get; }
    }
}