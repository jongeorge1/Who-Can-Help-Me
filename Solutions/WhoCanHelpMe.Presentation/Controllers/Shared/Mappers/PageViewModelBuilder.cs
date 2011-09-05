namespace WhoCanHelpMe.Presentation.Controllers.Shared.Mappers
{
    #region Using Directives

    using Domain.Contracts.Tasks;
    using Contracts;
    using ViewModels;

    using WhoCanHelpMe.Domain.Contracts.Configuration;

    #endregion

    public class PageViewModelBuilder : IPageViewModelBuilder
    {
        private readonly IAnalyticsConfiguration analyticsConfiguration;

        public PageViewModelBuilder(IAnalyticsConfiguration analyticsConfiguration)
        {
            this.analyticsConfiguration = analyticsConfiguration;
        }

        public PageViewModel Get()
        {
            var viewModel = new PageViewModel();

            return this.UpdateSiteProperties(viewModel);
        }

        public T UpdateSiteProperties<T>(T pageViewModel) where T : PageViewModel
        {
            pageViewModel.AnalyticsIdentifier = this.analyticsConfiguration.Idenfitier;
            pageViewModel.SiteVerification = this.analyticsConfiguration.Verification;

            return pageViewModel;
        }
    }
}