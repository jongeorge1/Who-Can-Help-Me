namespace WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using Shared.Mappers.Contracts;
    using Shared.ViewModels;

    using SharpArch.Futures.Core.Mapping;

    #endregion

    public abstract class BasePageViewModelMapper<TInput, TOutput> : Mapper<TInput, TOutput>
        where TOutput : PageViewModel
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        protected BasePageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public override TOutput MapFrom(TInput input)
        {
            return this.pageViewModelBuilder.UpdateSiteProperties(base.MapFrom(input));
        }
    }
}