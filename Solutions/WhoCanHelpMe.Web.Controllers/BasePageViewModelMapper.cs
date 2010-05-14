namespace WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using Framework.Mapper;

    using Shared.Mappers.Contracts;
    using Shared.ViewModels;

    #endregion

    public abstract class BasePageViewModelMapper<TInput, TOutput> : BaseMapper<TInput, TOutput>
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

    public abstract class BasePageViewModelMapper<TInput1, TInput2, TOutput> : BaseMapper<TInput1, TInput2, TOutput>
    where TOutput : PageViewModel
    {
        private readonly IPageViewModelBuilder pageViewModelBuilder;

        protected BasePageViewModelMapper(IPageViewModelBuilder pageViewModelBuilder)
        {
            this.pageViewModelBuilder = pageViewModelBuilder;
        }

        public override TOutput MapFrom(TInput1 input1, TInput2 input2)
        {
            return this.pageViewModelBuilder.UpdateSiteProperties(base.MapFrom(input1, input2));
        }
    }
}