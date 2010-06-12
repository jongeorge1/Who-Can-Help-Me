namespace WhoCanHelpMe.Web.Controllers.Search
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using ViewModels;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Framework.Mapper;

    #endregion

    public class SearchController : BaseController
    {
        private readonly IMapper<IList<Assertion>, IList<Tag>, SearchResultsPageViewModel> searchResultsPageViewModelMapper;

        private readonly ISearchTasks searchTasks;

        private readonly IMapper<IList<Tag>, SearchPageViewModel> searchPageViewModelMapper;

        private readonly ITagTasks tagTasks;

        public SearchController(
            ISearchTasks searchTasks,
            ITagTasks tagTasks,
            IMapper<IList<Tag>, SearchPageViewModel> searchPageViewModelMapper,
            IMapper<IList<Assertion>, IList<Tag>, SearchResultsPageViewModel> searchResultsPageViewModelMapper)
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