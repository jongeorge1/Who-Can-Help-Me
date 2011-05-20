namespace WhoCanHelpMe.Presentation.Controllers.Shared.Mappers
{
    #region Using Directives

    using Domain.Contracts.Tasks;
    using Contracts;
    using ViewModels;

    #endregion

    public class PageViewModelBuilder : IPageViewModelBuilder
    {
        private readonly ISiteMetaDataTasks siteMetaDataTasks;

        public PageViewModelBuilder(ISiteMetaDataTasks siteMetaDataTasks)
        {
            this.siteMetaDataTasks = siteMetaDataTasks;
        }

        public PageViewModel Get()
        {
            var viewModel = new PageViewModel();

            return this.UpdateSiteProperties(viewModel);
        }

        public T UpdateSiteProperties<T>(T pageViewModel) where T : PageViewModel
        {
            var siteMetaData = this.siteMetaDataTasks.GetSiteMetaData();

            pageViewModel.AnalyticsIdentifier = siteMetaData.AnalyticsIdentifier;
            pageViewModel.SiteVerification = siteMetaData.SiteVerification;

            return pageViewModel;
        }
    }
}