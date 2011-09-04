namespace WhoCanHelpMe.Presentation.Controllers.Tag
{
    using System.Linq;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

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
            var matchingTags = this.tagTasks.GetWhereNameStartsWith(q);
            var resultStrings = matchingTags.Select(t => t.Name).ToArray();
            var result = string.Join("\n", resultStrings);

            return new ContentResult 
                {
                    Content = result
                };
        }
    }
}