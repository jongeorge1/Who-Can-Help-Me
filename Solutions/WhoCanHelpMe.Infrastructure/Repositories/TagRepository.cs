namespace WhoCanHelpMe.Infrastructure.Repositories
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Linq;

    using Domain;
    using Domain.Contracts.Repositories;

    using NHibernate;

    #endregion

    public class TagRepository : LinqRepository<Tag>, ITagRepository
    {
        public IEnumerable<Tag> PopularTags(int count)
        {
            return this.FindAll().OrderByDescending(tag => tag.Views).Take(count);
        }
    }
}
