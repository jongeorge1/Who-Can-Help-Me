namespace WhoCanHelpMe.Web.Controllers.Home
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    using Shared.ViewModels;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Framework.Mapper;
    using WhoCanHelpMe.Web.Controllers.Home.ViewModels;

    #endregion

    public class HomeController : BaseController
    {
        private static readonly ICacheKey IndexInnerCacheKey = new CacheKey(CacheName.AdHoc, "HomeController.IndexInner");

        private readonly IMapper<IList<NewsItem>, HomePageViewModel> homePageViewModelMapper;

        private readonly ICachingService cachingService;

        private readonly INewsTasks newsTasks;

        public HomeController(
            INewsTasks newsTasks,
            IMapper<IList<NewsItem>, HomePageViewModel> homePageViewModelMapper,
            ICachingService cachingService)
        {
            this.newsTasks = newsTasks;
            this.homePageViewModelMapper = homePageViewModelMapper;
            this.cachingService = cachingService;
        }

        public ActionResult Index()
        {
            var pageViewModel = this.IndexInner();

            return this.View(pageViewModel);
        }

        private PageViewModel IndexInner()
        {
            var viewModel = this.cachingService[IndexInnerCacheKey] as PageViewModel;

            if (viewModel == null)
            {
                lock (IndexInnerCacheKey)
                {
                    viewModel = this.cachingService[IndexInnerCacheKey] as PageViewModel;
                    if (viewModel == null)
                    {
                        var buzz = this.newsTasks.GetProjectBuzz();
                        viewModel = this.homePageViewModelMapper.MapFrom(buzz);
                        this.cachingService.Add(IndexInnerCacheKey, viewModel);
                    }
                }
            }

            return viewModel;
        }
    }
}