namespace WhoCanHelpMe.Infrastructure.News
{
	#region Using Directives

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;

	using Domain;
	using Domain.Contracts.Configuration;
	using Domain.Contracts.Services;
	using Framework.Extensions;
	
	// Provides access to features like relative time, and casting from XML/JSON to
	// Twitter data classes
	using TweetSharp.Twitter.Extensions;
	
	// Provides access to the fluent API; required
	using TweetSharp.Twitter.Fluent;

	#endregion

	public class TwitterNewsService : INewsService
	{
		private readonly IConfigurationService configurationService;

		public TwitterNewsService(IConfigurationService configurationService)
		{
			this.configurationService = configurationService;
		}

		public IList<NewsItem> GetHeadlines()
		{
			var noOfHeadlinesToRetrieve = configurationService.News.NoOfBuzzHeadlines;
			var searchTags = configurationService.News.BuzzHeadlineTags;
			var searchTimeout = configurationService.News.SearchTimeoutSeconds;

			return GetMatchingHeadlines(searchTags, searchTimeout, noOfHeadlinesToRetrieve, this.SearchForHashTags);
		}

		public IList<NewsItem> GetDevTeamHeadlines()
		{
			var noOfHeadlinesToRetrieve = configurationService.News.NoOfDevTeamHeadlines;
			var searchTags = configurationService.News.DevTeamHeadlineTags;
			var searchTimeout = configurationService.News.SearchTimeoutSeconds;

			return GetMatchingHeadlines(searchTags, searchTimeout, noOfHeadlinesToRetrieve, this.SearchForUser);
		}

		private IList<NewsItem> GetMatchingHeadlines(IEnumerable<string> searchTags, int searchTimeout, int noOfHeadlinesToRetrieve, Action<string, IList<NewsItem>> searchDelegate)
		{
			var results = new List<NewsItem>();
			var waitHandles = new List<WaitHandle>();
			
			foreach (var currentTag in searchTags)
			{
				waitHandles.Add(searchDelegate.BeginInvoke(currentTag, results, null, null).AsyncWaitHandle);
			}

			// TODO: Logging if the timeout is hit
			WaitHandle.WaitAll(waitHandles.ToArray(), TimeSpan.FromSeconds(searchTimeout));

			return results.OrderByDescending(o => o.PublishedTime)
				.Distinct()
				.Take(noOfHeadlinesToRetrieve)
				.ToList();
		}

		private void SearchForHashTags(string tag, IList<NewsItem> results)
		{
			var search = FluentTwitter.CreateRequest()
									  .Search()
									  .Query()
									  .ContainingHashTag(tag);

			MapResults(search, results);
		}

		private void SearchForUser(string tag, IList<NewsItem> results)
		{
			var search = FluentTwitter.CreateRequest()
									  .Search()
									  .Query()
									  .FromUser(tag);

			MapResults(search, results);
		}

		private void MapResults(ITwitterSearchQuery search, IList<NewsItem> results)
		{
			var r = search.Request()
						  .AsSearchResult();

			results.AddRange(r.Statuses.ConvertAll(t => new NewsItem
															{
																Author = t.FromUserScreenName,
																AuthorPhotoUrl = t.ProfileImageUrl,
																AuthorUrl = "http://twitter.com/{0}".FormatWith(t.FromUserScreenName),
																PublishedTime = t.CreatedDate,
																Headline = t.Text,
																Url = t.Source
															}));
		}
	}
}