using WhoCanHelpMe.Web.Controllers.Text.Mappers;

namespace WhoCanHelpMe.Web.Controllers.Text
{
    #region Using Directives

    using System.Web.Mvc;
    using Domain.Cms.Pages;
    using N2.Web;

    #endregion

    [Controls(typeof(TextPage))]
    public class TextController : N2Controller<TextPage>
    {
        private readonly ITextPageViewModelMapper textPageViewModelMapper;

        public TextController(ITextPageViewModelMapper textPageViewModelMapper)
        {
            this.textPageViewModelMapper = textPageViewModelMapper;
        }

        public override ActionResult Index()
        {
            return View(textPageViewModelMapper.MapFrom(CurrentItem));
        }
    }
}