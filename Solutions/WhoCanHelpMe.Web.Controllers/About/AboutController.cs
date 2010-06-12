namespace WhoCanHelpMe.Web.Controllers.About
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Aspects.Caching;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    using Shared.ViewModels;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Framework.Mapper;
    using WhoCanHelpMe.Web.Controllers.About.ViewModels;

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
            var pageViewModel = this.IndexInner();

            return this.View(pageViewModel);
        }

        [Cached(CacheName.AdHoc)]
        private PageViewModel IndexInner()
        {
            var developmentTeamBuzz = this.newsTasks.GetDevelopmentTeamBuzz();

            return this.aboutPageViewModelMapper.MapFrom(developmentTeamBuzz);
        }

        public ActionResult DemonstrateErrorHandling()
        {
            throw new NotImplementedException("This action method has purposely been left empty.");
        }
    }
}