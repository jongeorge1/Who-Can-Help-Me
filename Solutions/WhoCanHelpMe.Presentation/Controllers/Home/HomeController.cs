namespace WhoCanHelpMe.Presentation.Controllers.Home
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using SharpArch.Futures.Core.Mapping;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Presentation.Controllers.Home.ViewModels;

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
            var buzz = this.newsTasks.GetProjectBuzz();
            var viewModel = this.homePageViewModelMapper.MapFrom(buzz);

            return this.View(viewModel);
        }
    }
}