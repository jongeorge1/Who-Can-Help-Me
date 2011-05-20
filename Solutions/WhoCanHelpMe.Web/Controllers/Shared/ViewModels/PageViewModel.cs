namespace WhoCanHelpMe.Web.Controllers.Shared.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Base view model for page views
    /// </summary>
    public class PageViewModel
    {
        public PageViewModel()
        {
            this.MetaData = new MetaDataViewModel();
        }

        public string AnalyticsIdentifier { get; set; }

        public MetaDataViewModel MetaData { get; set; }

        public string SiteVerification { get; set; }
    }
}