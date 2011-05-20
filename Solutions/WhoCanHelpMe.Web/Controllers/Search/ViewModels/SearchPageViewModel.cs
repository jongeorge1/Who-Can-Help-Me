namespace WhoCanHelpMe.Web.Controllers.Search.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;

    using Shared.ViewModels;

    #endregion

    public class SearchPageViewModel : PageViewModel
    {
        public SearchPageViewModel()
        {
            this.PopularTags = new List<TagViewModel>();
        }

        public IList<TagViewModel> PopularTags { get; set; }
    }
}