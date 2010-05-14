using N2;
using N2.Web;
using WhoCanHelpMe.Domain.Cms.Pages;

namespace MSpecTests.WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using global::WhoCanHelpMe.Domain;
    using global::WhoCanHelpMe.Domain.Contracts.Repositories;
    using global::WhoCanHelpMe.Domain.Contracts.Tasks;
    using global::WhoCanHelpMe.Domain.Specifications;
    using global::WhoCanHelpMe.Tasks;

    using Machine.Specifications;
    using Machine.Specifications.AutoMocking.Rhino;
    using Rhino.Mocks;

    #endregion

    public abstract class specification_for_cms_tasks : Specification<ICmsTasks, CmsTasks>
    {
        protected static IUrlParser n2Repository;

        Establish context = () =>
        {
            n2Repository = DependencyOf<IUrlParser>();
        };
    }

    public class when_the_cms_tasks_is_asked_for_the_navigation_items : specification_for_cms_tasks
    {
        static IList<ContentItem> result;

        Establish context = () =>
            {
                n2Repository.Stub(x => x.StartPage).Return(An<HomePage>());
                n2Repository.StartPage.Children = new List<ContentItem>{new TextPage(), new TextPage()};
            };

        Because of = () => result = subject.GetNavigationItems();

        It should_ask_the_cms_repository_for_all_the_top_level_content_items = () =>
            result.Count.ShouldEqual(2);
    }
}
