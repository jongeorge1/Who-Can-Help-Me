namespace WhoCanHelpMe.Presentation.Controllers.About
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using SharpArch.Futures.Core.Mapping;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Presentation.Controllers.About.ViewModels;

    #endregion

    public class AboutController : BaseController
    {
        private readonly IMapper<IList<NewsItem>, AboutPageViewModel> aboutPageViewModelMapper;

        private readonly INewsTasks newsTasks;

        public AboutController(
            INewsTasks newsTasks,
            IMapper<IList<NewsItem>, AboutPageViewModel> aboutPageViewModelMapper)
        {
            this.newsTasks = newsTasks;
            this.aboutPageViewModelMapper = aboutPageViewModelMapper;
        }

        public ActionResult Index()
        {
            var buzz = this.newsTasks.GetDevelopmentTeamBuzz();
            var viewModel = this.aboutPageViewModelMapper.MapFrom(buzz);

            return this.View(viewModel);
        }

        public ActionResult DemonstrateErrorHandling()
        {
            throw new NotImplementedException("This action method has purposely been left empty.");
        }
    }
}