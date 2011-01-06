namespace WhoCanHelpMe.Tasks.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    using WhoCanHelpMe.Domain;

    #endregion

    public interface ITagQueryTasks
    {
        IList<Tag> GetWhereNameStartsWith(string characters);

        IList<Tag> GetMostPopularTags(int count);

        Tag GetByName(string name);
    }
}
