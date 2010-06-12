namespace MSpecTests.WhoCanHelpMe.Web.Controllers
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Web.Mvc;

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Framework.Mapper;
    using global::WhoCanHelpMe.Web.Controllers.Search;
    using global::WhoCanHelpMe.Web.Controllers.Search.ViewModels;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Machine.Specifications.Mvc;
    using Rhino.Mocks;

    #endregion

    public  class specification_for_search_controller : Specification<SearchController>
    {
        protected static ITagTasks tag_tasks;
        protected static List<Tag> the_popular_tags;
        public static ISearchTasks search_tasks;
        protected static IMapper<IList<Tag>, SearchPageViewModel> search_view_model_mapper;
        protected static IMapper<IList<Assertion>, IList<Tag>, SearchResultsPageViewModel> search_results_view_model_mapper;

        Establish context= () =>
        {
            search_tasks = DependencyOf<ISearchTasks>();
            tag_tasks = DependencyOf<ITagTasks>();
            search_view_model_mapper = DependencyOf<IMapper<IList<Tag>, SearchPageViewModel>>();
            search_results_view_model_mapper = DependencyOf<IMapper<IList<Assertion>, IList<Tag>, SearchResultsPageViewModel>>();

            the_popular_tags = new List<Tag>();

            tag_tasks.Stub(tt => tt.GetMostPopularTags(10)).Return(the_popular_tags);

            ServiceLocatorHelper.AddCachingService();
        };
    }

    [Subject(typeof(SearchController))]
    public class when_the_search_controller_is_asked_for_the_default_view : specification_for_search_controller
    {
        protected static ActionResult result;
        static SearchPageViewModel the_view_model;

        Establish context= () =>
            {
                the_view_model = new SearchPageViewModel();

                search_view_model_mapper.Stub(hvmm => hvmm.MapFrom(the_popular_tags)).Return(the_view_model);
            };

        Because of = () => result = subject.Index();

        It should_return_the_default_view = () =>
            result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_get_the_popular_tags_from_the_tag_tasks =
            () => tag_tasks.AssertWasCalled(tt => tt.GetMostPopularTags(10));

        It should_get_the_view_model_from_the_search_view_model_mapper =
            () => search_view_model_mapper.AssertWasCalled(hvmm => hvmm.MapFrom(the_popular_tags));

        It should_pass_the_view_model_to_the_view =
            () => result.Model<SearchPageViewModel>().ShouldBeTheSameAs(the_view_model);

    }

    [Subject(typeof(SearchController))]
    public class when_the_search_controller_is_asked_to_search_by_tag_name : specification_for_search_controller
    {
        static string the_searched_tag_name;
        static SearchResultsPageViewModel the_view_model;
        static IList<Assertion> the_matching_assertions;
        static ActionResult result;

        Establish context = () =>
        {
            the_searched_tag_name = "my test tag";
            the_view_model = new SearchResultsPageViewModel();

            search_tasks.Stub(st => st.ByTag(the_searched_tag_name)).Return(the_matching_assertions);

            search_results_view_model_mapper.Stub(srvmm => srvmm.MapFrom(the_matching_assertions, the_popular_tags)).Return(the_view_model);
        };

        Because of = () => result = subject.ByTag(the_searched_tag_name);

        It should_return_the_search_results_by_tag_view = () =>
            result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_get_the_view_model_from_the_search_results_view_model_mapper =
            () => search_results_view_model_mapper.AssertWasCalled(hvmm => hvmm.MapFrom(the_matching_assertions, the_popular_tags));

        It should_pass_the_view_model_to_the_view =
            () => result.Model<SearchResultsPageViewModel>().ShouldBeTheSameAs(the_view_model);

        It should_get_the_popular_tags_from_the_tag_tasks =
            () => tag_tasks.AssertWasCalled(tt => tt.GetMostPopularTags(10));

        It should_get_the_matching_assertions_from_the_search_tasks =
            () => search_tasks.AssertWasCalled(ss => ss.ByTag(the_searched_tag_name));
    }
}