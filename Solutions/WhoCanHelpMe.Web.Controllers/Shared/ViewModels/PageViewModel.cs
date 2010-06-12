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
            this.Scripts = new List<string>();
            this.Styles = new List<string>();
        }

        public string AnalyticsIdentifier { get; set; }

        public MetaDataViewModel MetaData { get; set; }

        public IList<string> Scripts { get; set; }

        public string SiteVerification { get; set; }

        public IList<string> Styles { get; set; }

        public string WebTitle { get; set; }
    }
}