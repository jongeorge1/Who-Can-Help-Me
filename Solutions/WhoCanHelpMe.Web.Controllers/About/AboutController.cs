namespace WhoCanHelpMe.Web.Controllers.About
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Domain.Contracts.Tasks;

    using Framework.Caching;

    using Shared.ViewModels;

    using WhoCanHelpMe.Domain;
    using WhoCanHelpMe.Framework.Mapper;
    using WhoCanHelpMe.Web.Controllers.About.ViewModels;

    #endregion

    public class AboutController : BaseController
    {
        private static readonly ICacheKey IndexInnerCacheKey = new CacheKey(CacheName.AdHoc, "AboutController.IndexInner");

        private readonly IMapper<IList<NewsItem>, AboutPageViewModel> aboutPageViewModelMapper;

        private readonly INewsTasks newsTasks;

        private readonly ICachingService cachingService;

        public AboutController(
            INewsTasks newsTasks,
            IMapper<IList<NewsItem>, AboutPageViewModel> aboutPageViewModelMapper, 
            ICachingService cachingService)
        {
            this.newsTasks = newsTasks;
            this.cachingService = cachingService;
            this.aboutPageViewModelMapper = aboutPageViewModelMapper;
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
                        var buzz = this.newsTasks.GetDevelopmentTeamBuzz();
                        viewModel = this.aboutPageViewModelMapper.MapFrom(buzz);
                        this.cachingService.Add(IndexInnerCacheKey, viewModel);
                    }
                }
            }

            return viewModel;
        }

        public ActionResult DemonstrateErrorHandling()
        {
            throw new NotImplementedException("This action method has purposely been left empty.");
        }
    }
}