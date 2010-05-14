namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface ITagTasks
    {
        IList<Tag> GetWhereNameStartsWith(string characters);

        IList<Tag> GetMostPopularTags(int count);

        Tag GetByName(string name);
    }
}
