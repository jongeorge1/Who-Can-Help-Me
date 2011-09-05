namespace WhoCanHelpMe.Infrastructure.News
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using TweetSharp.Twitter.Extensions;
    using TweetSharp.Twitter.Fluent;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Domain.Contracts.Services;
    using WhoCanHelpMe.Framework.Extensions;
    using WhoCanHelpMe.Infrastructure.News.Contracts;
        // Provides access to features like relative time, and casting from XML/JSON to
        // Twitter data classes
        // Provides access to the fluent API; required

    #endregion

    public class TwitterNewsService : INewsService
    {
        private readonly INewsConfiguration newsConfiguration;

        public TwitterNewsService(INewsConfiguration newsConfiguration)
        {
            this.newsConfiguration = newsConfiguration;
        }

        public IList<NewsItem> GetDevTeamHeadlines()
        {
            var noOfHeadlinesToRetrieve = this.newsConfiguration.NoOfDevTeamHeadlines;
            var searchTags = this.newsConfiguration.DevTeamHeadlineTags;
            var searchTimeout = this.newsConfiguration.SearchTimeoutSeconds;

            return GetMatchingHeadlines(searchTags, searchTimeout, noOfHeadlinesToRetrieve, SearchForUser);
        }

        public IList<NewsItem> GetHeadlines()
        {
            var noOfHeadlinesToRetrieve = this.newsConfiguration.NoOfBuzzHeadlines;
            var searchTags = this.newsConfiguration.BuzzHeadlineTags;
            var searchTimeout = this.newsConfiguration.SearchTimeoutSeconds;

            return GetMatchingHeadlines(searchTags, searchTimeout, noOfHeadlinesToRetrieve, SearchForHashTags);
        }

        private static IList<NewsItem> GetMatchingHeadlines(
            IEnumerable<string> searchTags,
            int searchTimeout,
            int noOfHeadlinesToRetrieve,
            Action<string, IList<NewsItem>> searchDelegate)
        {
            var results = new List<NewsItem>();

            // TODO: Logging if the timeout is hit
            WaitHandle.WaitAll(
                searchTags.Select(
                    currentTag => searchDelegate.BeginInvoke(currentTag, results, null, null).AsyncWaitHandle).ToArray(),
                TimeSpan.FromSeconds(searchTimeout));

            return results.OrderByDescending(o => o.PublishedTime).Distinct().Take(noOfHeadlinesToRetrieve).ToList();
        }

        private static void MapResults(ITwitterSearchQuery search, IList<NewsItem> results)
        {
            var r = search.Request().AsSearchResult();

            results.AddRange(
                r.Statuses.ConvertAll(
                    t =>
                    new NewsItem
                        {
                            Author = t.FromUserScreenName,
                            AuthorPhotoUrl = t.ProfileImageUrl,
                            AuthorUrl = "http://twitter.com/{0}".FormatWith(t.FromUserScreenName),
                            PublishedTime = t.CreatedDate,
                            Headline = t.Text,
                            Url = t.Source
                        }));
        }

        private static void SearchForHashTags(string tag, IList<NewsItem> results)
        {
            var search = FluentTwitter.CreateRequest().Search().Query().ContainingHashTag(tag);

            MapResults(search, results);
        }

        private static void SearchForUser(string tag, IList<NewsItem> results)
        {
            var search = FluentTwitter.CreateRequest().Search().Query().FromUser(tag);

            MapResults(search, results);
        }
    }
}