namespace WhoCanHelpMe.Web.Controllers.Tag
{
    using System.Linq;
    using System.Web.Mvc;

    using Aspects.Caching;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    public class TagController : BaseController
    {
        private readonly ITagTasks tagTasks;

        public TagController(ITagTasks tagTasks)
        {
            this.tagTasks = tagTasks;
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

        [Cached(CacheName.AdHoc)]
        private string StartingWithInner(string value)
        {
            var result = this.tagTasks.GetWhereNameStartsWith(value);

            var resultStrings = result.ToList().ConvertAll(t => t.Name).ToArray();

            return string.Join("\n", resultStrings);
        }
    }
}