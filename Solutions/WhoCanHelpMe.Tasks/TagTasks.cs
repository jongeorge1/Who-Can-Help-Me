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

    public class TagTasks : ITagTasks
    {
        private readonly ITagRepository tagRepository;

        public TagTasks(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public IList<Tag> GetWhereNameStartsWith(string characters)
        {
            if (string.IsNullOrEmpty(characters))
            {
                return new List<Tag>();
            }

            return this.DoSearch(new TagByFirstCharactersOfNameSpecification(characters));
        }

        public IList<Tag> GetMostPopularTags(int count)
        {
            return this.tagRepository
                       .FindAll()
                       .OrderByDescending(tag => tag.Views)
                       .Take(count)
                       .ToList();
        }

        public Tag GetByName(string name)
        {
            return this.DoSearch(new TagByNameSpecification(name))
                       .FirstOrDefault();
        }

        private IList<Tag> DoSearch(QuerySpecification<Tag> specification)
        {
            return this.tagRepository
                       .FindAll(specification)
                       .OrderBy(t => t.Name)
                       .ToList();
        }
    }
}
