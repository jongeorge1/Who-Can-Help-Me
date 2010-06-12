namespace WhoCanHelpMe.Web.Controllers.Home
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using Aspects.Caching;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    using Shared.ViewModels;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Framework.Mapper;
    using WhoCanHelpMe.Web.Controllers.Home.ViewModels;

    #endregion

    public class HomeController : BaseController
    {
        private readonly IMapper<IList<NewsItem>, HomePageViewModel> homePageViewModelMapper;

        private readonly INewsTasks newsTasks;

        public HomeController(
            INewsTasks newsTasks,
            IMapper<IList<NewsItem>, HomePageViewModel> homePageViewModelMapper)
        {
            this.newsTasks = newsTasks;
            this.homePageViewModelMapper = homePageViewModelMapper;
        }

        public ActionResult Index()
        {
            var pageViewModel = this.IndexInner();

            return this.View(pageViewModel);
        }

        [Cached(CacheName.AdHoc)]
        private PageViewModel IndexInner()
        {
            var buzz = this.newsTasks.GetProjectBuzz();

            return this.homePageViewModelMapper.MapFrom(buzz);
        }
    }
}