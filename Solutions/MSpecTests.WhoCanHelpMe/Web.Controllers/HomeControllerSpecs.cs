namespace MSpecTests.WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Cms.Pages;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Web.Controllers.Home;
    using global::WhoCanHelpMe.Web.Controllers.Home.Mappers.Contracts;
    using global::WhoCanHelpMe.Web.Controllers.Home.ViewModels;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Machine.Specifications.Mvc;
    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_home_controller : Specification<HomeController>
    {
        protected static IHomePageViewModelMapper home_view_model_mapper;
        protected static INewsTasks news_tasks;
        protected static HomePage home_page;

        Establish context = () =>
        {
            var routeData = new RouteData();
            home_page = new HomePage();
            routeData.Values.Add("n2_item", home_page);

            home_view_model_mapper = DependencyOf<IHomePageViewModelMapper>();
            news_tasks = DependencyOf<INewsTasks>();

            ServiceLocatorHelper.AddCachingService();

            subject.ControllerContext = new ControllerContext() { RouteData = routeData };
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
                home_view_model_mapper.Stub(hvmm => hvmm.MapFrom(the_news_items, home_page)).Return(the_view_model);
            };
        
        Because of = () => result = subject.Index();

        It should_return_the_default_view = () =>
            result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_map_the_home_page_content_and_the_list_of_news_items_the_view_model =
            () => home_view_model_mapper.AssertWasCalled(hvmm => hvmm.MapFrom(the_news_items, home_page));

        It should_pass_the_view_model_to_the_view =
            () => result.Model<HomePageViewModel>().ShouldBeTheSameAs(the_view_model);
    }
}
