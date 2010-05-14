namespace WhoCanHelpMe.Domain.Contracts.Repositories
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface ITagRepository : ILinqRepository<Tag>
    {
        IEnumerable<Tag> PopularTags(int count);
    }
}
