namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public class SiteMetaData
    {
        public SiteMetaData()
        {
            this.Scripts = new List<string>();
            this.Styles = new List<string>();
        }

        public string AnalyticsIdentifier { get; set; }

        public string SiteVerification { get; set; }

        public IList<string> Scripts { get; set; }

        public IList<string> Styles { get; set; }

        public string Title { get; set; }
    }
}