using System.Collections.Generic;

namespace WhoCanHelpMe.Web.Controllers.Navigation.ViewModels
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            CmsLinks = new List<LinkViewModel>();
        }

        public bool IsLoggedIn { get; set; }

        public IList<LinkViewModel> CmsLinks { get; set; }
    }
}