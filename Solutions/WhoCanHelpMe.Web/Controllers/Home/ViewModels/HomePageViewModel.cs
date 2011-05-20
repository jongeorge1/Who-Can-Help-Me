namespace WhoCanHelpMe.Web.Controllers.Home.ViewModels
{
    #region Using Directives

    using System.Collections.Generic;

    using Shared.ViewModels;

    #endregion

    public class HomePageViewModel : PageViewModel
    {
        public HomePageViewModel()
        {
            this.NewsItems = new List<NewsItemViewModel>();
        }

        public IList<NewsItemViewModel> NewsItems { get; set; }
    }
}