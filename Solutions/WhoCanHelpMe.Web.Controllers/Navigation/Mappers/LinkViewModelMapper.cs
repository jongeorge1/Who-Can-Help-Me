using N2;
using WhoCanHelpMe.Web.Controllers.Navigation.ViewModels;

namespace WhoCanHelpMe.Web.Controllers.Navigation.Mappers
{
    public class LinkViewModelMapper : ILinkViewModelMapper
    {
        public LinkViewModel MapFrom(ContentItem input)
        {
            return new LinkViewModel
                       {
                           Title = input.Title,
                           Url = input.Url
                       };
        }
    }
}