using System;
using WhoCanHelpMe.Domain.Cms.Pages;
using WhoCanHelpMe.Web.Controllers.Shared.Mappers.Contracts;
using WhoCanHelpMe.Web.Controllers.Text.Model;

namespace WhoCanHelpMe.Web.Controllers.Text.Mappers
{
    public class TextPageViewModelMapper :  BasePageViewModelMapper<TextPage,TextPageViewModel>,
                                            ITextPageViewModelMapper
    {
        public TextPageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder) : base(pageViewModelBuilder)
        {
        }
    }
}