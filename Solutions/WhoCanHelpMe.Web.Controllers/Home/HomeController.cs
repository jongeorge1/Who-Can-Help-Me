namespace WhoCanHelpMe.Web.Controllers.Home
{
    #region Using Directives

    using System.Web.Mvc;

    using Aspects.Caching;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    using Mappers.Contracts;

    using Shared.ViewModels;

    #endregion

    public class HomeController : BaseController
    {
        private readonly IHomePageViewModelMapper homePageViewModelMapper;

        private readonly INewsTasks newsTasks;

        public HomeController(
            INewsTasks newsTasks,
            IHomePageViewModelMapper homePageViewModelMapper)
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