namespace WhoCanHelpMe.Web.Controllers.Search
{
    #region Using Directives

    using System.Web.Mvc;

    using Aspects.Caching;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    using Mappers.Contracts;

    using ViewModels;

    #endregion

    public class SearchController : BaseController
    {
        private readonly ISearchResultsPageViewModelMapper searchResultsPageViewModelMapper;

        private readonly ISearchTasks searchTasks;

        private readonly ISearchPageViewModelMapper searchPageViewModelMapper;

        private readonly ITagTasks tagTasks;

        public SearchController(
            ISearchTasks searchTasks,
            ITagTasks tagTasks,
            ISearchPageViewModelMapper searchPageViewModelMapper,
            ISearchResultsPageViewModelMapper searchResultsPageViewModelMapper)
        {
            this.searchTasks = searchTasks;
            this.tagTasks = tagTasks;
            this.searchPageViewModelMapper = searchPageViewModelMapper;
            this.searchResultsPageViewModelMapper = searchResultsPageViewModelMapper;
        }

        public ActionResult ByTag(string tagName)
        {
            var matchingAssertions = this.searchTasks.ByTag(tagName);
            var popularTags = this.tagTasks.GetMostPopularTags(10);

            var viewModel = this.searchResultsPageViewModelMapper.MapFrom(
                matchingAssertions,
                popularTags);

            viewModel.SearchTerm = tagName;

            return this.View(viewModel);
        }

        public ActionResult Index()
        {
            // Get most popular tags
            var popularTags = this.tagTasks.GetMostPopularTags(10);

            var viewModel = this.searchPageViewModelMapper.MapFrom(popularTags);

            return this.View(viewModel);
        }
    }
}