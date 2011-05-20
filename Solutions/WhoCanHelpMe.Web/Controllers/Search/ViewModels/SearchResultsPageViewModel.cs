namespace WhoCanHelpMe.Web.Controllers.Search.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public class SearchResultsPageViewModel : SearchPageViewModel
    {
        public IList<AssertionViewModel> MatchingAssertions { get; set; }

        public string SearchTerm { get; set; }
    }
}