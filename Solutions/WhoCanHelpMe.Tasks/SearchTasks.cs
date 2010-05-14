namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using Domain;
    using Domain.Contracts.Repositories;
    using Domain.Contracts.Tasks;
    using Domain.Specifications;

    #endregion

    public class SearchTasks : ISearchTasks
    {
        private readonly IAssertionRepository assertionRepository;

        private readonly ITagRepository tagRepository;

        public SearchTasks(
            IAssertionRepository assertionRepository,
            ITagRepository tagRepository)
        {
            this.assertionRepository = assertionRepository;
            this.tagRepository = tagRepository;
        }

        public IList<Assertion> ByTag(string tagName)
        {
            // Find the tag in question
            var tag = this.tagRepository.FindOne(new TagByNameSpecification(tagName));

            if (tag != null)
            {
                // Update the view count for the tag
                tag.Views++;
                this.tagRepository.Save(tag);

                // Find and return matching assertions
                return this.assertionRepository
                           .FindAll(new AssertionByTagIdSpecification(tag.Id))
                           .OrderBy(o => o.Profile.LastName)
                           .ToList();
            }

            return new List<Assertion>(0);
        }
    }
}
