namespace WhoCanHelpMe.Web.Controllers.Home.ViewModels
{
    #region Using Directives

    using System.Diagnostics;

    #endregion

    [DebuggerDisplay("{Headline}")]
    public class NewsItemViewModel
    {
        public string Author { get; set; }

        public string AuthorPhotoUrl { get; set; }

        public string AuthorUrl { get; set; }

        public string Headline { get; set; }

        public string Url { get; set; }
    }
}