namespace MSpecTests.WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Framework.Mapper;
    using global::WhoCanHelpMe.Web.Controllers.Home;
    using global::WhoCanHelpMe.Web.Controllers.Home.ViewModels;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Machine.Specifications.Mvc;
    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_home_controller : Specification<HomeController>
    {
        protected static IMapper<IList<NewsItem>, HomePageViewModel> home_view_model_mapper;
        protected static INewsTasks news_tasks;

        Establish context = () =>
        {
            home_view_model_mapper = DependencyOf<IMapper<IList<NewsItem>, HomePageViewModel>>();
            news_tasks = DependencyOf<INewsTasks>();

            ServiceLocatorHelper.AddCachingService();
        };
    }

    [Subject(typeof(HomeController))]
    public class when_the_home_controller_is_asked_for_the_default_view : specification_for_home_controller
    {
        static HomePageViewModel the_view_model;
        static IList<NewsItem> the_news_items;
        static ActionResult result;

        Establish context = () =>
            {
                the_view_model = new HomePageViewModel();
                the_news_items = new List<NewsItem>();

                news_tasks.Stub(nt => nt.GetProjectBuzz()).Return(the_news_items);
                home_view_model_mapper.Stub(hvmm => hvmm.MapFrom(the_news_items)).Return(the_view_model);
            };
        
        Because of = () => result = subject.Index();

        It should_return_the_default_view = () =>
            result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_get_the_view_model_from_the_home_view_model_mapper =
            () => home_view_model_mapper.AssertWasCalled(hvmm => hvmm.MapFrom(the_news_items));

        It should_pass_the_view_model_to_the_view =
            () => result.Model<HomePageViewModel>().ShouldBeTheSameAs(the_view_model);
    }
}
