namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface ISearchTasks
    {
        IList<Assertion> ByTag(string tagName);
    }
}
