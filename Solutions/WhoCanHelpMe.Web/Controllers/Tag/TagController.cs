namespace WhoCanHelpMe.Web.Controllers.Tag
{
    using System.Linq;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    public class TagController : BaseController
    {
        private const string CacheKeyBase = "TagController.StartingWithInner:";

        private static readonly object StartingWithInnerSyncRoot = new object();

        private readonly ITagTasks tagTasks;

        private readonly ICachingService cachingService;

        public TagController(ITagTasks tagTasks, ICachingService cachingService)
        {
            this.tagTasks = tagTasks;
            this.cachingService = cachingService;
        }

        /// <summary>
        /// AJAX method called by jQuery.Autocomplete.js
        /// </summary>
        /// <remarks>
        /// Parameter has to be called q - as this is specified in
        /// jquery.automcomplete.js line 338.
        /// </remarks>
        /// <param name="q">Tag to find</param>
        /// <returns>Array of results matching the specified tag</returns>
        public ActionResult StartingWith(string q)
        {
            return new ContentResult 
                {
                    Content = this.StartingWithInner(q)
                };
        }

        private string StartingWithInner(string value)
        {
            var cacheKey = new CacheKey(CacheName.AdHoc, CacheKeyBase + value);

            var result = this.cachingService[cacheKey] as string;

            if (result == null)
            {
                lock (StartingWithInnerSyncRoot)
                {
                    result = this.cachingService[cacheKey] as string;

                    if (result == null)
                    {
                        var matchingTags = this.tagTasks.GetWhereNameStartsWith(value);
                        var resultStrings = matchingTags.Select(t => t.Name).ToArray();
                        result = string.Join("\n", resultStrings);
                        this.cachingService.Add(cacheKey, result);
                    }
                }

            }

            return result;
        }
    }
}